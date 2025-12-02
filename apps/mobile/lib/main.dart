// 🐦 Flutter imports:
import 'package:flutter/material.dart';
import 'package:mobile/app.dart';
// 🌎 Project imports:
import 'package:mobile/core/config/common/enum/enums.dart';
import 'package:mobile/core/config/common/environment_variables.dart';
import 'package:mobile/core/config/sentry_config.dart';
import 'package:mobile/initialization.dart';

Future<void> main() async {
  EnvironmentVariables.configure(Flavor.Prod);
  await preInitializations();
  await SentryConfig.init(() => runApp(const MobileApp()));
}
