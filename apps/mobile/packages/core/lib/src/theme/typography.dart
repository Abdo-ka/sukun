part of "app_theme.dart";

TextTheme textTheme = TextTheme(
  displayLarge: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 57.sp,
      fontWeight: FontWeight.w400),
  displayMedium: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 42.sp,
      fontWeight: FontWeight.w400,
      height: 40.fromFigmaHeight(32)),
  displaySmall: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 32.sp,
      fontWeight: FontWeight.w400),

  /// headline
  headlineLarge: TextStyle(
    fontFamily: 'NotoKufiArabic',
    fontSize: 32.sp,
    fontWeight: FontWeight.w600,
    color: const Color(0xff023047),
    height: 1.2,
  ),

  headlineMedium: TextStyle(
    fontFamily: 'NotoKufiArabic',
    fontSize: 28.sp,
    fontWeight: FontWeight.w600,
    color: const Color(0xff023047),
    height: 32.fromFigmaHeight(28),
  ),
  headlineSmall: TextStyle(
    fontFamily: 'NotoKufiArabic',
    fontSize: 24.sp,
    fontWeight: FontWeight.w600,
    letterSpacing: -0.5.w,
    color: const Color(0xff023047),
    height: 32.fromFigmaHeight(24),
  ),

  ///Title
  titleLarge: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 20.0.sp,
      fontWeight: FontWeight.w400),
  titleMedium: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 18.0.sp,
      fontWeight: FontWeight.w400),
  titleSmall: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 16.0.sp,
      fontWeight: FontWeight.w500),

  ///Label
  labelLarge: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 14.0.sp,
      fontWeight: FontWeight.w500),
  labelMedium: TextStyle(
    fontFamily: 'NotoKufiArabic',
    fontSize: 12.0.sp,
    fontWeight: FontWeight.w500,
    height: 24.fromFigmaHeight(18),
  ),
  labelSmall: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 11.0.sp,
      fontWeight: FontWeight.w500),

  ///Body
  bodyLarge: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 16.0.sp,
      fontWeight: FontWeight.w400),
  bodyMedium: TextStyle(
    fontFamily: 'NotoKufiArabic',
    fontSize: 14.0.sp,
    fontWeight: FontWeight.w500,
    height: 24.fromFigmaHeight(16),
  ),
  bodySmall: TextStyle(
      fontFamily: 'NotoKufiArabic',
      fontSize: 12.0.sp,
      fontWeight: FontWeight.w400),
);

// // //?  Design text styles
// // extension TextThemeExtension on TextTheme {
// //   TextStyle get sumImageText => TextStyle(
// //       fontFamily: 'NotoKufiArabic',
// //       fontWeight: FontWeight.normal,
// //       fontSize: 16.sp,
// //       height: 17.fromFigmaHeight(16));

// //   TextStyle get paragraphMedium => TextStyle(
// //       fontFamily: 'NotoKufiArabic',
// //       fontWeight: FontWeight.normal,
// //       fontSize: 14.sp,
// //       height: 20.fromFigmaHeight(14));

// //   TextStyle get buttonText => TextStyle(
// //         fontFamily: 'NotoKufiArabic',
// //         fontWeight: FontWeight.w500,
// //         fontSize: 14.sp,
// //         height: 20.fromFigmaHeight(14),
// //       );

// //   TextStyle get regularText => TextStyle(
// //         fontFamily: 'NotoKufiArabic',
// //         fontWeight: FontWeight.w500,
// //         letterSpacing: -0.5.w,
// //         fontSize: 14.sp,
// //         height: 18.fromFigmaHeight(14),
// //       );

// //   TextStyle get listTitle => TextStyle(
// //         fontFamily: 'NotoKufiArabic',
// //         fontWeight: FontWeight.w600,
// //         fontSize: 16.sp,
// //         letterSpacing: -0.5.w,
// //         height: 16.fromFigmaHeight(16),
// //       );

// //   TextStyle get informerText => TextStyle(
// //         fontFamily: 'NotoKufiArabic',
// //         fontWeight: FontWeight.w500,
// //         fontSize: 12.sp,
// //         letterSpacing: -0.5.w,
// //         height: 16.fromFigmaHeight(12),
// //       );

// //   TextStyle get ratingText => TextStyle(
// //         fontFamily: 'NotoKufiArabic',
// //         fontWeight: FontWeight.w600,
// //         fontSize: 14.sp,
// //         letterSpacing: -0.5.w,
// //         height: 18.9.fromFigmaHeight(14),
// //       );

// //   TextStyle get ratingNumber => TextStyle(
// //         fontFamily: 'NotoKufiArabic',
// //         fontWeight: FontWeight.w500,
// //         fontSize: 10.sp,
// //         letterSpacing: -0.5.w,
// //         height: 13.5.fromFigmaHeight(10),
// //       );

//   ///            <<< NEW TEXT STYLE >>
//   /// this text is need to set weight[]  from [FamilyUtils] extension
//   TextStyle get display => TextStyle(
//         fontFamily: 'NotoKufiArabic',
//         fontSize: 57.sp,
//         letterSpacing: -0.16.w,
//         height: 20.fromFigmaHeight(32),
//       );

//   TextStyle get heading => TextStyle(
//         fontFamily: 'NotoKufiArabic',
//         fontSize: 32.sp,
//         letterSpacing: -0.16.w,
//         height: 32.fromFigmaHeight(24),
//       );

//   TextStyle get label => TextStyle(
//         fontFamily: 'NotoKufiArabic',
//         fontSize: 14.sp,
//         letterSpacing: -0.16.w,
//         height: 24.fromFigmaHeight(18),
//       );

//   TextStyle get body => TextStyle(
//         fontFamily: 'NotoKufiArabic',
//         fontSize: 16.sp,
//         letterSpacing: -0.16.w,
//         height: 24.fromFigmaHeight(16),
//       );

//   TextStyle get paragraph => TextStyle(
//         fontFamily: 'NotoKufiArabic',
//         fontSize: 14.sp,
//         letterSpacing: -0.16.w,
//         height: 20.fromFigmaHeight(14),
//       );

//   TextStyle get xs => TextStyle(
//         fontFamily: 'NotoKufiArabic',
//         fontSize: 12.sp,
//         height: 18.fromFigmaHeight(12),
//       );
// }

extension FamilyUtils on TextStyle {
  TextStyle get extraBold =>
      copyWith(fontWeight: FontWeight.w800);

  TextStyle get bold =>
      copyWith(fontWeight: FontWeight.bold);

  TextStyle get semiBold =>
      copyWith(fontWeight: FontWeight.w600);

  TextStyle get medium =>
      copyWith(fontWeight: FontWeight.w500);

  TextStyle get regular =>
      copyWith(fontWeight: FontWeight.w400);

  TextStyle get light =>
      copyWith(fontWeight: FontWeight.w300);
}

class HelperFont {
  static FontWeight? w440 = FontWeight.lerp(
      FontWeight.w400, FontWeight.w500, 0.4);

  static FontWeight? w430 = FontWeight.lerp(
      FontWeight.w400, FontWeight.w500, 0.3);

  static FontWeight? w460 = FontWeight.lerp(
      FontWeight.w400, FontWeight.w500, 0.6);

  static FontWeight? w428 = FontWeight.lerp(
      FontWeight.w400, FontWeight.w500, 0.28);

  static FontWeight? w472 = FontWeight.lerp(
      FontWeight.w400, FontWeight.w500, 0.72);

  static FontWeight? w536 = FontWeight.lerp(
      FontWeight.w500, FontWeight.w600, 0.36);
}
