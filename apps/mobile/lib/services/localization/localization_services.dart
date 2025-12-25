// 🐦 Flutter imports:
// 📦 Package imports:
import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
// 🌎 Project imports:
import 'package:mobile/core/utils/app_localization.dart';

class LocalizationServices extends StatelessWidget {
  const LocalizationServices({
    super.key,
    required this.child,
  });
  final Widget child;
  @override
  Widget build(BuildContext context) {
    return EasyLocalization(
      supportedLocales: AppLocalization.supportedLocales,
      path: 'assets/translations',
      fallbackLocale: AppLocalization.fallbackLocale,
      startLocale: AppLocalization.arLocale,
      saveLocale: true,
      child: child,
    );
  }
}
