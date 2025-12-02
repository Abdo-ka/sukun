// 🐦 Flutter imports:
import 'package:flutter/material.dart';
// 📦 Package imports:
import 'package:flutter_screenutil/flutter_screenutil.dart';

class AppTypography {
  static TextTheme get textTheme => TextTheme(
    displayLarge: TextStyle(
      fontSize: 57.sp,
      fontWeight: FontWeight.bold,
      fontFamily: notoKufiArabic,
    ),
    displayMedium: TextStyle(
      fontSize: 42.sp,
      fontFamily: notoKufiArabic,
    ),
    displaySmall: TextStyle(
      fontSize: 32.sp,
      fontFamily: notoKufiArabic,
    ),
    headlineLarge: TextStyle(
      fontSize: 32.sp,
      fontFamily: notoKufiArabic,
      fontWeight: FontWeight.w700,
    ),
    headlineMedium: TextStyle(
      fontSize: 28.sp,
      fontFamily: notoKufiArabic,
      fontWeight: FontWeight.w700,
    ),
    headlineSmall: TextStyle(
      fontSize: 24.sp,
      fontFamily: notoKufiArabic,
    ),
    titleLarge: TextStyle(
      fontSize: 20.sp,
      fontFamily: notoKufiArabic,
    ),
    titleMedium: TextStyle(
      fontSize: 18.sp,
      fontFamily: notoKufiArabic,
    ),
    titleSmall: TextStyle(
      fontSize: 16.sp,
      fontFamily: notoKufiArabic,
    ),
    bodyLarge: TextStyle(
      fontSize: 16.sp,
      fontWeight: FontWeight.w400,
      fontFamily: notoKufiArabic,
    ),
    bodyMedium: TextStyle(
      fontSize: 14.sp,
      fontFamily: notoKufiArabic,
    ),
    bodySmall: TextStyle(
      fontSize: 12.sp,
      fontFamily: notoKufiArabic,
    ),
    labelLarge: TextStyle(
      fontSize: 14.sp,
      fontFamily: notoKufiArabic,
    ),
    labelMedium: TextStyle(
      fontSize: 12.sp,
      fontFamily: notoKufiArabic,
    ),
    labelSmall: TextStyle(
      fontSize: 11.sp,
      fontFamily: notoKufiArabic,
    ),
  );

  static const String notoKufiArabic = 'NotoKufiArabic';
}
