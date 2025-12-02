import 'package:flutter/material.dart';

extension ColorSchemeExtension on ColorScheme {
  Color get main =>
      getColor(light: const Color(0xffFB5607), dark: const Color(0xffFB5607));

  Color get offWhite =>
      getColor(light: const Color(0xffFAF2EF), dark: const Color(0xffFAF2EF));

  Color get textTitle =>
      getColor(light: const Color(0xff1E1E1E), dark: const Color(0xff1E1E1E));

  Color get textColor =>
      getColor(light: const Color(0xff757684), dark: const Color(0xff757684));

  Color get star5 =>
      getColor(light: const Color(0xff41933a), dark: const Color(0xff476E2F));

  Color get star4 =>
      getColor(light: const Color(0xff688034), dark: const Color(0xff688034));

  Color get star3 =>
      getColor(light: const Color(0xffEDD614), dark: const Color(0xffEDD614));

  Color get star2 =>
      getColor(light: const Color(0xffFF7900), dark: const Color(0xffFF7900));

  Color get star1 =>
      getColor(light: const Color(0xffCB212E), dark: const Color(0xffCB212E));

  Color get heading =>
      getColor(light: const Color(0xff023047), dark: const Color(0xff023047));

  Color get border3 =>
      getColor(light: const Color(0xffD9D9D9), dark: const Color(0xffD9D9D9));

  Color get border1 => getColor(
      light: const Color.fromARGB(65, 228, 228, 228),
      dark: const Color.fromARGB(65, 228, 228, 228));

  Color get blur => getColor(
      light: const Color.fromARGB(102, 43, 45, 66),
      dark: const Color.fromARGB(102, 43, 45, 66));

  Color get blur1 => getColor(
      light: const Color.fromRGBO(0, 0, 0, 0.16),
      dark: const Color.fromRGBO(0, 0, 0, 0.16));

  Color get border => getColor(
      light: const Color.fromARGB(51, 255, 255, 255),
      dark: const Color.fromARGB(51, 255, 255, 255));

  Color get outlineColor =>
      getColor(light: const Color(0xffE4E4E4), dark: const Color(0xffE4E4E4));

  Color get backgroundColor =>
      getColor(light: const Color(0xffF3F3F3), dark: const Color(0xffF3F3F3));

  MaterialColor get systemGray => const MaterialColor(
        0xff667085,
        <int, Color>{
          25: Color(0xffFCFCFD),
          50: Color(0xffF9FAFB),
          100: Color(0xffF2F4F7),
          200: Color(0xffEAECF0),
          300: Color(0xffD0D5DD),
          400: Color(0xff98A2B3),
          500: Color(0xff667085),
          600: Color(0xff475467),
          700: Color(0xff344054),
          800: Color(0xff1D2939),
          900: Color(0xff101828),
        },
      );

  MaterialColor get systemSuccess => const MaterialColor(
        0xff12B76A,
        <int, Color>{
          25: Color(0xffF6FEF9),
          50: Color(0xffECFDF3),
          100: Color(0xffD1FADF),
          200: Color(0xffA6F4C5),
          300: Color(0xff6CE9A6),
          400: Color(0xff32D583),
          500: Color(0xff12B76A),
          600: Color(0xff039855),
          700: Color(0xff027A48),
          800: Color(0xff05603A),
        },
      );

  static MaterialColor get systemWarning => const MaterialColor(
        0xffF79009,
        <int, Color>{
          25: Color(0xffFFFCF5),
          50: Color(0xffFFFAEB),
          100: Color(0xffFEF0C7),
          200: Color(0xffFEDF89),
          300: Color(0xffFEC84B),
          400: Color(0xffFDB022),
          500: Color(0xffF79009),
          600: Color(0xffDC6803),
          700: Color(0xffB54708),
          800: Color(0xff93370D),
          900: Color(0xff7A2E0E),
        },
      );

  static MaterialColor get systemError => const MaterialColor(
        0xffF04438,
        <int, Color>{
          25: Color(0xffFFFBFA),
          50: Color(0xffFEF3F2),
          100: Color(0xffFEE4E2),
          200: Color(0xffFECDCA),
          300: Color(0xffFDA29B),
          400: Color(0xffF97066),
          500: Color(0xffF04438),
          600: Color(0xffD92D20),
          700: Color(0xffB42318),
          800: Color(0xff912018),
        },
      );

  MaterialColor get brandPrimary => const MaterialColor(
        0xffBC4105,
        <int, Color>{
          25: Color(0xffFFF3EE),
          50: Color(0xffFEE6DA),
          100: Color(0xffFECBB2),
          200: Color(0xffFB5607),
          300: Color(0xffE24D06),
          400: Color(0xffC94506),
          500: Color(0xffBC4105),
          600: Color(0xff973404),
          700: Color(0xff712703),
          800: Color(0xff581E02),
        },
      );

  MaterialColor get brandAccent => const MaterialColor(
        0xff222636,
        <int, Color>{
          50: Color(0xffF0F0F5),
          100: Color(0xffBEC0C9),
          200: Color(0xff8D8F9C),
          300: Color(0xff5C5E6F),
          400: Color(0xff2B2D42),
          500: Color(0xff222636),
          600: Color(0xff1A1D29),
          700: Color(0xff12141C),
          800: Color(0xff0A0B0F),
        },
      );

  // static const MaterialColor brandPrimary = MaterialColor(
  //   0xff,
  //   <int, Color>{
  //     25: Color(0xff),
  //     50: Color(0xff),
  //     100: Color(0xff),
  //     200: Color(0xff),
  //     300: Color(0xff),
  //     400: Color(0xff),
  //     500: Color(0xff),
  //     600: Color(0xff),
  //     700: Color(0xff),
  //     800: Color(0xff),
  //   },
  // );

  Color shadowCard1(double opacity) {
    return getColor(
      light: Color.fromRGBO(16, 24, 40, opacity),
      dark: Color.fromRGBO(16, 24, 40, opacity),
    );
  }

  Color shadowCard2(double opacity) {
    return getColor(
        light: Color.fromRGBO(33, 165, 105, opacity),
        dark: Color.fromRGBO(33, 165, 105, opacity));
  }

  Color shadowCard3(double opacity) {
    return getColor(
        light: Color.fromRGBO(255, 255, 255, opacity),
        dark: Color.fromRGBO(255, 255, 255, opacity));
  }

  Color shadowCard4(double opacity) {
    return getColor(
        light: Color.fromRGBO(251, 103, 33, opacity),
        dark: Color.fromRGBO(251, 103, 33, opacity));
  }

  ///Helper Method
  Color getColor({required Color light, required Color dark}) =>
      brightness == Brightness.light ? light : dark;
}

extension FFF on MaterialColor {
  Color get shade25 => this[25]!;
}
