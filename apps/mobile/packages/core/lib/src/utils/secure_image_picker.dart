import 'dart:io';

import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter_image_compress/flutter_image_compress.dart';
import 'package:image_cropper/image_cropper.dart';
import 'package:image_picker/image_picker.dart';
import 'package:mime/mime.dart';
import 'package:path_provider/path_provider.dart';
import 'package:uuid/uuid.dart';

class SecureFilePicker {
  static const filenameLengthLimit = 20;
  static const fileSizeLimit = 4000000; // 4MB
  static const int defaultQuality = 85;
  static const int minQuality = 40;
  static const int qualityStep = 10;

  // Cache for last compressed file to avoid redundant compression
  static File? _lastSourceFile;
  static File? _lastCompressedFile;

  static Future<File?> pickImage(ImageSource source,
      {CropAspectRatioPreset? cropAspectRatio}) async {
    try {
      final ImagePicker picker = ImagePicker();
      final pickedFile = await picker.pickImage(
        source: source,
        maxWidth: 1920,
        maxHeight: 1920,
        imageQuality: defaultQuality,
      );

      if (pickedFile == null) return null;

      final file = File(pickedFile.path);
      if (!file.existsSync()) return null;

      final File compressedFile = await _compressAndGetFile(file);

      if (!validateFile(compressedFile)) return null;

      // needs cropping
      if (cropAspectRatio != null) {
        final croppedFile = await _cropImage(compressedFile,
            aspectRatioPreset: cropAspectRatio);
        return croppedFile;
      }

      return compressedFile;
    } catch (e) {
      // Log error or handle gracefully
      return null;
    }
  }

  static Future<File?> pickWebImage(String userId) async {
    FilePickerResult? result =
        await FilePicker.platform.pickFiles(type: FileType.image);

    if (result != null) {
      PlatformFile fileBytes = result.files.first;
      final file = File(fileBytes.path ?? '');

      final compressedFile =
          await _compressAndGetFile(file, targetSize: fileSizeLimit);

      if (!validateFile(compressedFile)) return null;

      return compressedFile;
    }

    return null;
  }

  static bool validateFile(File file) {
    // Check file exists first
    if (!file.existsSync()) return false;

    // Check file size
    try {
      if (file.lengthSync() > fileSizeLimit) return false;
    } catch (e) {
      return false;
    }

    // Check actual file type
    final mimeType = lookupMimeType(file.path);
    if (mimeType == null || !mimeType.startsWith('image/')) return false;

    return true;
  }

  static Future<File> _compressAndGetFile(File sourceFile,
      {int? targetSize}) async {
    // Check cache to avoid redundant compression
    if (_lastSourceFile?.path == sourceFile.path &&
        _lastCompressedFile != null) {
      if (_lastCompressedFile!.existsSync()) {
        final cachedSize = _lastCompressedFile!.lengthSync();
        if (targetSize == null || cachedSize < targetSize) {
          return _lastCompressedFile!;
        }
      }
    }

    final dir = await getTemporaryDirectory();
    final targetPath = '${dir.absolute.path}/${const Uuid().v4()}.jpg';

    // Start with higher quality for better results
    for (int quality = defaultQuality;
        quality >= minQuality;
        quality -= qualityStep) {
      final data = await FlutterImageCompress.compressAndGetFile(
        sourceFile.absolute.path,
        targetPath,
        quality: quality,
        minWidth: 1920,
        minHeight: 1080,
        format: CompressFormat.jpeg,
      );

      if (data == null) continue;

      final file = File(data.path);

      if (targetSize == null) {
        _lastSourceFile = sourceFile;
        _lastCompressedFile = file;
        return file;
      }

      if (file.lengthSync() < targetSize) {
        _lastSourceFile = sourceFile;
        _lastCompressedFile = file;
        return file;
      }
    }

    throw Exception('Image is too large, cannot compress below target size');
  }

  static Future<File?> _cropImage(final File file,
      {CropAspectRatioPreset aspectRatioPreset =
          CropAspectRatioPreset.ratio16x9}) async {
    CroppedFile? croppedFile = await ImageCropper().cropImage(
      sourcePath: file.path,
      uiSettings: [
        AndroidUiSettings(
          toolbarTitle: 'Cropper',
          toolbarColor: const Color(0xff497174), //TODO MAKE IT CUSTOMIZABLE
          toolbarWidgetColor: Colors.white, //TODO MAKE IT CUSTOMIZABLE
          initAspectRatio: aspectRatioPreset,
          lockAspectRatio: true, //TODO MAKE IT CUSTOMIZABLE
        ),
        IOSUiSettings(title: 'Cropper'),
      ],
    );
    if (croppedFile == null) {
      return null;
    }

    return File(croppedFile.path);
  }
}
