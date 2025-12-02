part of 'app_theme.dart';

extension AppButtonTheme on AppTheme {
  TextButtonThemeData textButtonTheme({required bool isDark}) => TextButtonThemeData(
        style: TextButton.styleFrom(
            // shadowColor: Colors.red,
            // backgroundColor:,
            // disabledBackgroundColor:,
            // foregroundColor:,
            // disabledForegroundColor:,

            //   surfaceTintColor:
            //       isDark ? darkCustomColors.title : darkCustomColors.title,
            //   iconColor: isDark ? darkCustomColors.title : darkCustomColors.title,
            //   textStyle: isDark
            //       ? AppFont.darkTextTheme.labelMedium
            //           ?.copyWith(fontSize: 18.sp, color: darkCustomColors.title)
            //       : AppFont.lightTextTheme.labelMedium
            //           ?.copyWith(fontSize: 18.sp, color: darkCustomColors.title),
            //   padding: PEdgeInsets.allS3,
            shape: _shape),
      );

  ElevatedButtonThemeData elevatedButtonTheme(ColorScheme scheme) => ElevatedButtonThemeData(
        style: ElevatedButton.styleFrom(
            backgroundColor: scheme.primary,
            foregroundColor: scheme.surface,
            // disabledBackgroundColor:,
            // foregroundColor:,
            // disabledForegroundColor:,

            //   fixedSize: Size.fromHeight(56.h),
            //   textStyle: isDark
            //       ? AppFont.darkTextTheme.labelMedium
            //           ?.copyWith(fontSize: 18.sp, color: darkColorScheme.onPrimary)
            //       : AppFont.lightTextTheme.labelMedium?.copyWith(
            //           fontSize: 18.sp, color: lightColorScheme.onPrimary),
            //   padding: PEdgeInsets.allS3,
            shape: _shape),
      );

  OutlinedButtonThemeData outlinedButtonTheme({required bool isDark}) => OutlinedButtonThemeData(
        style: OutlinedButton.styleFrom(
          // backgroundColor:,
          // disabledBackgroundColor:,
          // foregroundColor:,
          // disabledForegroundColor:,

          //     textStyle: isDark
          //         ? AppFont.darkTextTheme.labelMedium
          //             ?.copyWith(fontSize: 18.sp, color: darkColorScheme.primary)
          //         : AppFont.lightTextTheme.labelMedium?.copyWith(
          //             fontSize: 18.sp, color: lightColorScheme.primary),
          //     padding: PEdgeInsets.horizontalL1,
          shape: _shape,
          //     side: BorderSide(
          //       color:
          //           isDark ? darkColorScheme.primary : lightColorScheme.primary,
          //     )
        ),
      );

  FilledButtonThemeData filledButtonTheme({required bool isDark}) {
    return FilledButtonThemeData(
      style: FilledButton.styleFrom(
        // backgroundColor:,
        // disabledBackgroundColor:,
        // foregroundColor:,
        // disabledForegroundColor:,

        //   textStyle: isDark
        //       ? AppFont.darkTextTheme.labelMedium?.copyWith(fontSize: 18.sp)
        //       : AppFont.lightTextTheme.labelMedium?.copyWith(fontSize: 18.sp),
        //   padding: REdgeInsets.symmetric(
        //       vertical: LayoutConstrains.m3, horizontal: LayoutConstrains.l1),
        shape: _shape,
      ),
    );
  }

  // Color foregroundColor(bool isDark) =>
  //     isDark ? darkColorScheme.onPrimary : lightColorScheme.onPrimary;

  // Color _backgroundColor(bool isDark) =>
  //     isDark ? darkColorScheme.primary : lightColorScheme.primary;

  RoundedRectangleBorder get _shape => RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(_borderRadius),
      );

  double get _borderRadius => 8;
}
