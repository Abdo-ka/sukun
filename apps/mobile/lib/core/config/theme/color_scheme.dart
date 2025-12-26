// 🐦 Flutter imports:
import 'package:flutter/material.dart';

class AppColorScheme {
  static const light = ColorScheme.light(
    brightness: Brightness.light,
    surface: Color(0xffFDFDFD),
    onSurface: Color(0xff191C1D),
    primary: Color(0xffFF634D),
    onPrimary: Color(0xffFFFFFF),
    primaryContainer: Color(0xffFFEFED),
    onPrimaryContainer: Color(0xff6B2A20),
    secondary: Color(0xff34937D),
    onSecondary: Color(0xffFFFFFF),
    secondaryContainer: Color(0xffEBF6F3),
    onSecondaryContainer: Color(0xff18443A),
    tertiary: Color(0xff9BBD91),
    onTertiary: Color(0xffFFFFFF),
    tertiaryContainer: Color(0xffF7FAF5),
    onTertiaryContainer: Color(0xff475743),
    error: Color(0xffBA1A1A),
    onError: Color(0xffFFFFFF),
    errorContainer: Color(0xffFFDAD5),
    onErrorContainer: Color(0xff410002),
    inverseSurface: Color(0xff2E3132),
    onInverseSurface: Color(0xffEFF1F1),
    surfaceContainer: Color(0xFFECEEEF),
    surfaceTint: Color(0xff006B58),
    surfaceContainerHighest: Color(0xffDBE4E6),
    onSurfaceVariant: Color(0xff3F484A),
    outline: Color(0xff6F797A),
    outlineVariant: Color(0xffBFC8CA),
    inversePrimary: Color(0xffFFB7AD),
    shadow: Color(0xff191C1D),
    scrim: Color(0xff191C1D),
  );
  // static const lightStage = ColorScheme.light(
  //   brightness: Brightness.light,
  //   primary: Color(0xFF9747FF),
  //   onPrimary: Color(0xFFFDFFFA),
  //   primaryContainer: Color(0xffFFECCC),
  //   onPrimaryContainer: Color(0xff3A6012),
  //   secondary: Color(0xFF088EAA),
  //   secondaryContainer: Color(0xFFDCF2FF),
  //   onSecondary: Color(0xFFFFFFFF),
  //   error: Color(0xFFFF4000),
  //   onError: Color(0xFFFFFAF9),
  //   tertiary: Color(0xff287D0E),
  //   onTertiary: Color(0xFFFFFFFF),
  //   errorContainer: Color(0xFFFFEDE8),
  //   onErrorContainer: Color(0xFF87361D),
  //   surface: Color.fromARGB(255, 255, 255, 255),
  //   onSurface: Color(0xFF34313C),
  //   surfaceContainerHighest: Color(0xFFDEDDE1),
  //   onSurfaceVariant: Color(0xFF534F5C),
  //   outline: Color(0xffd9dce9),
  //   onInverseSurface: Color(0xFFEFF1F1),
  //   inverseSurface: Color(0xFF6C6E7B),
  //   shadow: Color(0xFF000000),
  // );
  // static const lightProd = ColorScheme.light(
  //   brightness: Brightness.light,
  //   primary: Color(0xFFFF0090),
  //   onPrimary: Color(0xFFFDFFFA),
  //   primaryContainer: Color(0xffFFECCC),
  //   onPrimaryContainer: Color(0xff3A6012),
  //   secondary: Color(0xFF088EAA),
  //   secondaryContainer: Color(0xFFDCF2FF),
  //   onSecondary: Color(0xFFFFFFFF),
  //   error: Color(0xFFFF4000),
  //   onError: Color(0xFFFFFAF9),
  //   tertiary: Color(0xff287D0E),
  //   onTertiary: Color(0xFFFFFFFF),
  //   errorContainer: Color(0xFFFFEDE8),
  //   onErrorContainer: Color(0xFF87361D),
  //   surface: Color.fromARGB(255, 255, 255, 255),
  //   onSurface: Color(0xFF34313C),
  //   surfaceContainerHighest: Color(0xFFDEDDE1),
  //   onSurfaceVariant: Color(0xFF534F5C),
  //   outline: Color(0xffd9dce9),
  //   onInverseSurface: Color(0xFFEFF1F1),
  //   inverseSurface: Color(0xFF6C6E7B),
  //   shadow: Color(0xFF000000),
  // );

  static const dark = ColorScheme.light(
    brightness: Brightness.dark,
    primary: Color(0xFF77AF23),
    onPrimary: Color(0xFFFDFFFA),
    primaryContainer: Color(0xffF9FFEE),
    onPrimaryContainer: Color(0xff3A6012),
    secondary: Color(0xFF277CAD),
    secondaryContainer: Color(0xFFDCF2FF),
    onSecondary: Color(0xFFFFFFFF),
    error: Color(0xFFF7744C),
    onError: Color(0xFFFFFAF9),
    tertiary: Color(0xff023047),
    onTertiary: Color(0xFFFFFFFF),
    errorContainer: Color(0xFFFFEDE8),
    onErrorContainer: Color(0xFF87361D),
    surface: Color.fromARGB(255, 255, 255, 255),
    onSurface: Color(0xFF34313C),
    surfaceContainerHighest: Color(0xFFDEDDE1),
    onSurfaceVariant: Color(0xFF534F5C),
    outline: Color(0xFF716E79),
    onInverseSurface: Color(0xFFEFF1F1),
    inverseSurface: Color(0xFF2D3132),
    shadow: Color(0xFF000000),
  );
}

extension TextColorScheme on ColorScheme {
  Color get primaryText => brightness == Brightness.light
      ? const Color(0xffEFF1F1)
      : const Color(0xff1E1F24);

  Color get secondaryText => brightness == Brightness.light
      ? const Color(0xff6C6E7B)
      : const Color(0xffEFF1F1);
}

extension ExtraColors on ColorScheme {
  Color get gray => brightness == Brightness.light
      ? const Color(0xfff1f1f1)
      : const Color(0xfff1f1f1);

  Color get iconGray => brightness == Brightness.light
      ? const Color(0xff6C6E7B)
      : const Color(0xff534F5C);

  Color get lightGreen => const Color(0xffE3F8CC);

  Color get lightBlue => const Color(0xffCAFAF4);
}

extension ColorTools on Color {
  Color darken([double amount = .1]) {
    assert(amount >= 0 && amount <= 1);

    final hsl = HSLColor.fromColor(this);
    final hslDark = hsl.withLightness(
      (hsl.lightness - amount).clamp(0.0, 1.0),
    );

    return hslDark.toColor();
  }

  Color lighten([double amount = .1]) {
    assert(amount >= 0 && amount <= 1);

    final hsl = HSLColor.fromColor(this);
    final hslLight = hsl.withLightness(
      (hsl.lightness + amount).clamp(0.0, 1.0),
    );

    return hslLight.toColor();
  }
}
