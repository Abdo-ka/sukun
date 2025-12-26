// dart format width=80

/// GENERATED CODE - DO NOT MODIFY BY HAND
/// *****************************************************
///  FlutterGen
/// *****************************************************

// coverage:ignore-file
// ignore_for_file: type=lint
// ignore_for_file: deprecated_member_use,directives_ordering,implicit_dynamic_list_literal,unnecessary_import

import 'package:flutter/widgets.dart';

class $AssetsFontsGen {
  const $AssetsFontsGen();

  /// File path: assets/fonts/Almarai-Bold.ttf
  String get almaraiBold => 'assets/fonts/Almarai-Bold.ttf';

  /// File path: assets/fonts/Almarai-ExtraBold.ttf
  String get almaraiExtraBold => 'assets/fonts/Almarai-ExtraBold.ttf';

  /// File path: assets/fonts/Almarai-Light.ttf
  String get almaraiLight => 'assets/fonts/Almarai-Light.ttf';

  /// File path: assets/fonts/Almarai-Regular.ttf
  String get almaraiRegular => 'assets/fonts/Almarai-Regular.ttf';

  /// List of all assets
  List<String> get values => [
    almaraiBold,
    almaraiExtraBold,
    almaraiLight,
    almaraiRegular,
  ];
}

class $AssetsIconsGen {
  const $AssetsIconsGen();

  /// File path: assets/icons/Others.svg
  String get others => 'assets/icons/Others.svg';

  /// File path: assets/icons/al-aser.svg
  String get alAser => 'assets/icons/al-aser.svg';

  /// File path: assets/icons/al-duhr.svg
  String get alDuhr => 'assets/icons/al-duhr.svg';

  /// File path: assets/icons/al-eshaa.svg
  String get alEshaa => 'assets/icons/al-eshaa.svg';

  /// File path: assets/icons/al-fajir.svg
  String get alFajir => 'assets/icons/al-fajir.svg';

  /// File path: assets/icons/al-maghrib.svg
  String get alMaghrib => 'assets/icons/al-maghrib.svg';

  /// File path: assets/icons/al-shuruq.svg
  String get alShuruq => 'assets/icons/al-shuruq.svg';

  /// File path: assets/icons/allah.svg
  String get allah => 'assets/icons/allah.svg';

  /// File path: assets/icons/arrow-left-square.svg
  String get arrowLeftSquare => 'assets/icons/arrow-left-square.svg';

  /// File path: assets/icons/ayah.svg
  String get ayah => 'assets/icons/ayah.svg';

  /// File path: assets/icons/azan_duher.svg
  String get azanDuher => 'assets/icons/azan_duher.svg';

  /// File path: assets/icons/azkar.svg
  String get azkar => 'assets/icons/azkar.svg';

  /// File path: assets/icons/dimond.svg
  String get dimond => 'assets/icons/dimond.svg';

  /// File path: assets/icons/doaa.svg
  String get doaa => 'assets/icons/doaa.svg';

  /// File path: assets/icons/haddith.svg
  String get haddith => 'assets/icons/haddith.svg';

  /// File path: assets/icons/hijab.png
  AssetGenImage get hijab => const AssetGenImage('assets/icons/hijab.png');

  /// File path: assets/icons/location.svg
  String get location => 'assets/icons/location.svg';

  /// File path: assets/icons/notification.svg
  String get notification => 'assets/icons/notification.svg';

  /// File path: assets/icons/qibla.png
  AssetGenImage get qibla => const AssetGenImage('assets/icons/qibla.png');

  /// File path: assets/icons/quran.svg
  String get quran => 'assets/icons/quran.svg';

  /// File path: assets/icons/saas_marks.svg
  String get saasMarks => 'assets/icons/saas_marks.svg';

  /// File path: assets/icons/setting.svg
  String get setting => 'assets/icons/setting.svg';

  /// File path: assets/icons/star.svg
  String get star => 'assets/icons/star.svg';

  /// File path: assets/icons/stories.svg
  String get stories => 'assets/icons/stories.svg';

  /// File path: assets/icons/sunan.svg
  String get sunan => 'assets/icons/sunan.svg';

  /// File path: assets/icons/woman.png
  AssetGenImage get woman => const AssetGenImage('assets/icons/woman.png');

  /// File path: assets/icons/zakat.png
  AssetGenImage get zakat => const AssetGenImage('assets/icons/zakat.png');

  /// List of all assets
  List<dynamic> get values => [
    others,
    alAser,
    alDuhr,
    alEshaa,
    alFajir,
    alMaghrib,
    alShuruq,
    allah,
    arrowLeftSquare,
    ayah,
    azanDuher,
    azkar,
    dimond,
    doaa,
    haddith,
    hijab,
    location,
    notification,
    qibla,
    quran,
    saasMarks,
    setting,
    star,
    stories,
    sunan,
    woman,
    zakat,
  ];
}

class $AssetsLottieGen {
  const $AssetsLottieGen();

  /// File path: assets/lottie/rose.json
  String get rose => 'assets/lottie/rose.json';

  /// List of all assets
  List<String> get values => [rose];
}

class $AssetsTranslationsGen {
  const $AssetsTranslationsGen();

  /// File path: assets/translations/ar-SY.json
  String get arSY => 'assets/translations/ar-SY.json';

  /// File path: assets/translations/en-US.json
  String get enUS => 'assets/translations/en-US.json';

  /// List of all assets
  List<String> get values => [arSY, enUS];
}

class Assets {
  const Assets._();

  static const $AssetsFontsGen fonts = $AssetsFontsGen();
  static const $AssetsIconsGen icons = $AssetsIconsGen();
  static const $AssetsLottieGen lottie = $AssetsLottieGen();
  static const $AssetsTranslationsGen translations = $AssetsTranslationsGen();
}

class AssetGenImage {
  const AssetGenImage(
    this._assetName, {
    this.size,
    this.flavors = const {},
    this.animation,
  });

  final String _assetName;

  final Size? size;
  final Set<String> flavors;
  final AssetGenImageAnimation? animation;

  Image image({
    Key? key,
    AssetBundle? bundle,
    ImageFrameBuilder? frameBuilder,
    ImageErrorWidgetBuilder? errorBuilder,
    String? semanticLabel,
    bool excludeFromSemantics = false,
    double? scale,
    double? width,
    double? height,
    Color? color,
    Animation<double>? opacity,
    BlendMode? colorBlendMode,
    BoxFit? fit,
    AlignmentGeometry alignment = Alignment.center,
    ImageRepeat repeat = ImageRepeat.noRepeat,
    Rect? centerSlice,
    bool matchTextDirection = false,
    bool gaplessPlayback = true,
    bool isAntiAlias = false,
    String? package,
    FilterQuality filterQuality = FilterQuality.medium,
    int? cacheWidth,
    int? cacheHeight,
  }) {
    return Image.asset(
      _assetName,
      key: key,
      bundle: bundle,
      frameBuilder: frameBuilder,
      errorBuilder: errorBuilder,
      semanticLabel: semanticLabel,
      excludeFromSemantics: excludeFromSemantics,
      scale: scale,
      width: width,
      height: height,
      color: color,
      opacity: opacity,
      colorBlendMode: colorBlendMode,
      fit: fit,
      alignment: alignment,
      repeat: repeat,
      centerSlice: centerSlice,
      matchTextDirection: matchTextDirection,
      gaplessPlayback: gaplessPlayback,
      isAntiAlias: isAntiAlias,
      package: package,
      filterQuality: filterQuality,
      cacheWidth: cacheWidth,
      cacheHeight: cacheHeight,
    );
  }

  ImageProvider provider({AssetBundle? bundle, String? package}) {
    return AssetImage(_assetName, bundle: bundle, package: package);
  }

  String get path => _assetName;

  String get keyName => _assetName;
}

class AssetGenImageAnimation {
  const AssetGenImageAnimation({
    required this.isAnimation,
    required this.duration,
    required this.frames,
  });

  final bool isAnimation;
  final Duration duration;
  final int frames;
}
