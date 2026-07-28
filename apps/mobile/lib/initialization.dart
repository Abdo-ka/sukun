// 🐦 Flutter imports:
// 📦 Package imports:
import 'dart:ui';

import 'package:easy_localization/easy_localization.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_crashlytics/firebase_crashlytics.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
// 🌎 Project imports:
import 'package:mobile/core/di/di_container.dart';
import 'package:mobile/services/crashlytics_service.dart';
import 'package:mobile/services/hive_service.dart';
import 'package:mobile/services/notification_service.dart';
import 'package:sentry_flutter/sentry_flutter.dart';

Future<void> preInitializations() async {
  SentryWidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp();
  FlutterError.onError = (errorDetails) {
    FirebaseCrashlytics.instance.recordFlutterFatalError(
      errorDetails,
    );
  };
  PlatformDispatcher.instance.onError = (error, stack) {
    FirebaseCrashlytics.instance.recordError(
      error,
      stack,
      fatal: true,
    );
    return true;
  };
  await configureDependencies();
  await HiveService.initialHive();
  await NotificationService.init();
  await Future.wait([
    EasyLocalization.ensureInitialized(),
    ScreenUtil.ensureScreenSize(),
  ]);
  EasyLocalization.logger.enableLevels = [];

  SystemChrome.setPreferredOrientations([
    DeviceOrientation.portraitUp,
    DeviceOrientation.portraitDown,
  ]);
  CrashlyticsService.captureError();
}
