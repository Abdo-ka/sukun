// 🐦 Flutter imports:
import 'package:flutter/material.dart';
import 'package:mobile/core/config/theme/app_typography.dart';
import 'package:mobile/core/config/theme/color_scheme.dart';

export 'app_typography.dart';
export 'color_scheme.dart';

class AppTheme {
  static ThemeData get light => ThemeData(
    brightness: Brightness.light,
    // useMaterial3: false,
    colorScheme: AppColorScheme.light,
    // EnvironmentVariables.flavor == Flavor.Dev
    //     ? AppColorScheme.lightDev
    //     : EnvironmentVariables.flavor == Flavor.Stag
    //     ? AppColorScheme.lightStage
    //     : AppColorScheme.lightProd,
    textTheme: AppTypography.textTheme,
    fontFamily: AppTypography.notoKufiArabic,
    appBarTheme: _appBarTheme(AppColorScheme.light),
    switchTheme: SwitchThemeData(
      thumbColor: WidgetStatePropertyAll(
        AppColorScheme.light.surface,
      ),
    ),
  );

  static ThemeData get dark => ThemeData(
    brightness: Brightness.dark,
    // useMaterial3: false,
    colorScheme: AppColorScheme.dark,
    textTheme: AppTypography.textTheme,
    fontFamily: AppTypography.notoKufiArabic,
    appBarTheme: _appBarTheme(AppColorScheme.dark),
    switchTheme: SwitchThemeData(
      thumbColor: WidgetStatePropertyAll(
        AppColorScheme.dark.surface,
      ),
    ),
  );

  static AppBarTheme _appBarTheme(ColorScheme scheme) =>
      AppBarTheme(
        backgroundColor: scheme.surface,
        centerTitle: true,
      );
}
