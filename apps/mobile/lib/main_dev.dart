// 🐦 Flutter imports:
import 'package:flutter/material.dart';
import 'package:mobile/app.dart';
// 🌎 Project imports:
import 'package:mobile/core/config/common/enum/enums.dart';
import 'package:mobile/core/config/common/environment_variables.dart';
import 'package:mobile/initialization.dart';

void main() async {
  EnvironmentVariables.configure(Flavor.Dev);
  await preInitializations();

  runApp(const MobileApp());
}
