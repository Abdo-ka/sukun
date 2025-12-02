// 🐦 Flutter imports:
import 'package:flutter/material.dart';

// 📦 Package imports:
import 'package:easy_localization/easy_localization.dart';

class AppLocalization {
  static const arLocale = Locale('ar', 'SY');
  static const enLocale = Locale('en', 'US');
  static List<Locale> get supportedLocales => [arLocale, enLocale];
  static Locale get fallbackLocale => enLocale;

  static bool isEnglish(BuildContext context) =>
      context.locale.languageCode == enLocale.languageCode;

  static Future<void> reverseLang(BuildContext context) => isEnglish(context)
      ? context.setLocale(arLocale)
      : context.setLocale(enLocale);
}
