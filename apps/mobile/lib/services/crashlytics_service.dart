// 🎯 Dart imports:
import 'dart:isolate';

// 🐦 Flutter imports:
import 'package:flutter/foundation.dart';

// 📦 Package imports:
import 'package:firebase_crashlytics/firebase_crashlytics.dart';

abstract class CrashlyticsService {
  static final _instance = FirebaseCrashlytics.instance;

  static void captureError() {
    // if (EnvironmentVariables.flavor==Flavor.Dev) {
    //   return;
    // }

    FlutterError.onError = _instance.recordFlutterFatalError;

    PlatformDispatcher.instance.onError = (error, stack) {
      _instance.recordError(error, stack, fatal: true);
      return true;
    };

    Isolate.current.addErrorListener(RawReceivePort((pair) async {
      final List<dynamic> errorAndStacktrace = pair;
      await _instance.recordError(
        errorAndStacktrace.first,
        errorAndStacktrace.last,
      );
    }).sendPort);
  }

  static Future<void> recordError(
    dynamic exception,
    StackTrace? stack, {
    dynamic reason,
    Iterable<Object> information = const [],
    bool? printDetails,
    bool fatal = false,
  }) async {
    // if (EnvironmentVariables.flavor==Flavor.Dev) {
    //   return;
    // }

    return _instance.recordError(
      exception,
      stack,
      reason: reason,
      information: information,
      printDetails: printDetails,
      fatal: fatal,
    );
  }
}
