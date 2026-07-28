// 🐦 Flutter imports:
// 📦 Package imports:
import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:flutter_blurhash/flutter_blurhash.dart';
import 'package:flutter_svg/flutter_svg.dart';

enum Source { assets, network }

class AppImage extends StatelessWidget {
  const AppImage.asset(
    this.path, {
    super.key,
    this.alignment,
    this.fit,
    this.height,
    this.width,
    this.color,
    this.colorFilter,
    this.loadingBuilder,
    this.failedBuilder,
    this.size,
    this.placeholder,
    this.ratio,
    this.blurHash,
  })  : _source = Source.assets,
        remoteImage = null;

  const AppImage.network({
    super.key,
    this.alignment,
    this.fit,
    this.height,
    this.width,
    this.color,
    this.loadingBuilder,
    this.failedBuilder,
    this.colorFilter,
    this.size,
    this.placeholder,
    this.remoteImage,
    this.ratio,
    String? path,
    this.blurHash,
  })  : _source = Source.network,
        path = remoteImage ?? path ?? '';

  final Source _source;

  final String path;
  final Alignment? alignment;
  final BoxFit? fit;
  final double? height;
  final double? width;
  final Color? color;
  final WidgetBuilder? loadingBuilder;
  final Widget Function(BuildContext context, String name)?
      placeholder;
  // final RemoteImage? remoteImage;
  final String? remoteImage;
  final String? blurHash;
  final double? ratio;

  ///pass size will overwrite height and width
  final double? size;

  ///this will be ignored if the image source is [SvgPicture.network]
  final WidgetBuilder? failedBuilder;

  ///only work on svg and color will be ignore
  final ColorFilter? colorFilter;

  @override
  Widget build(BuildContext context) {
    if (path.contains('.svg')) {
      switch (_source) {
        case Source.assets:
          return SvgPicture.asset(
            path,
            colorFilter: colorFilter ??
                (color != null
                    ? ColorFilter.mode(
                        color!, BlendMode.srcIn)
                    : null),
            alignment: alignment ?? Alignment.center,
            fit: fit ?? BoxFit.contain,
            height: getHeight(),
            width: getWidth(),
          );
        case Source.network:
          return SvgPicture.network(
            path,
            colorFilter: color != null
                ? ColorFilter.mode(color!, BlendMode.srcIn)
                : null,
            alignment: alignment ?? Alignment.center,
            fit: fit ?? BoxFit.contain,
            height: getHeight(),
            width: getWidth(),
          );
      }
    } else {
      switch (_source) {
        case Source.assets:
          return Image.asset(
            path,
            color: color,
            alignment: alignment ?? Alignment.center,
            fit: fit,
            height: getHeight(),
            width: getWidth(),
          );
        case Source.network:
          if (ratio != null) {
            return AspectRatio(
              aspectRatio: ratio!,
              child: CachedNetworkImage(
                imageUrl: path,
                color: color,
                placeholder: placeholder ??
                    (context, url) => BlurHash(
                          hash: blurHash == null ||
                                  blurHash!.isEmpty
                              ? 'L@IY97M{M_of~qRjRjax-.j[t8WB'
                              : blurHash!,
                          decodingHeight:
                              getHeight()?.toInt() ?? 50,
                          decodingWidth:
                              getWidth()?.toInt() ?? 50,
                        ),
                alignment: alignment ?? Alignment.center,
                fit: fit,
                height: getHeight(),
                width: getWidth(),
              ),
            );
          }
          return CachedNetworkImage(
            imageUrl: path,
            color: color,
            placeholder: placeholder ??
                (remoteImage != null
                    ? (context, url) => BlurHash(
                          hash: blurHash ?? '',
                          decodingHeight:
                              getHeight()?.toInt() ?? 50,
                          decodingWidth:
                              getWidth()?.toInt() ?? 50,
                        )
                    : null),
            alignment: alignment ?? Alignment.center,
            fit: fit,
            height: getHeight(),
            width: getWidth(),
          );
      }
    }
  }

  double? getHeight() => size ?? height;

  double? getWidth() => size ?? width;

  Widget getLoadingBuilder(BuildContext context) =>
      loadingBuilder != null
          ? loadingBuilder!(context)
          : SizedBox(
              height: getHeight(),
              width: getWidth(),
              child: const CircularProgressIndicator(
                  strokeWidth: 1),
            );

  Widget copyWith(Color? color) {
    return AppImage.asset(
      path,
      color: color ?? this.color,
      width: width,
      height: height,
      size: size,
      alignment: alignment,
      fit: fit,
      colorFilter: colorFilter,
      failedBuilder: failedBuilder,
      loadingBuilder: loadingBuilder,
      key: key,
    );
  }
}
