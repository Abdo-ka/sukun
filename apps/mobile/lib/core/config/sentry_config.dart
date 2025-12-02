import 'dart:async';

import 'package:mobile/core/config/common/environment_variables.dart';
import 'package:sentry_flutter/sentry_flutter.dart';

class SentryConfig {
  static Future<void> init(
    FutureOr<void> Function() appRunner,
  ) async {
    await SentryFlutter.init((options) {
      options.dsn = EnvironmentVariables.sentryDsn;
      options.environment =
          EnvironmentVariables.flavor.name;
      options.release = 'mobile@1.0.0+1';
      // Set tracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
      // We recommend adjusting this value in production.
      options.tracesSampleRate = 1.0;

      // The sampling rate for profiling is relative to tracesSampleRate
      // Setting to 1.0 will profile 100% of sampled transactions:
      options.profilesSampleRate = 1.0;

      options.attachStacktrace = true;
      options.enableAutoSessionTracking = true;

      // Enable user interaction tracing
      options.enableUserInteractionTracing = true;
    }, appRunner: appRunner);
  }
}
