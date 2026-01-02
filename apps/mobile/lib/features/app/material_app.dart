import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:mobile/core/config/theme/theme.dart';
import 'package:mobile/core/di/di_container.dart';
import 'package:mobile/features/app/app_update_wrapper.dart';
import 'package:mobile/features/app/responsive_layout.dart';
import 'package:mobile/services/router/router.dart';
import 'package:sentry_flutter/sentry_flutter.dart';

class MobileMaterialApp extends StatelessWidget {
  const MobileMaterialApp({super.key});

  @override
  Widget build(BuildContext context) {
    final router = getIt<AppRouter>();

    return MaterialApp.router(
      title: "Alrwda",
      debugShowCheckedModeBanner: false,
      localizationsDelegates: context.localizationDelegates,
      supportedLocales: context.supportedLocales,
      locale: context.locale,
      routerConfig: router.config(
        navigatorObservers: () => [
          SentryNavigatorObserver(),
        ],
      ),
      theme: AppTheme.light,
      themeMode: ThemeMode.light,
      builder: (context, child) => AppUpdateWrapper(
        child: ResponsiveLayout(child: child!),
      ),
    );
  }
}
