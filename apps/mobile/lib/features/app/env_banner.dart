import 'package:flutter/material.dart';
import 'package:mobile/core/config/common/environment_variables.dart';

class EnvBanner extends StatelessWidget {
  final Widget child;

  const EnvBanner({super.key, required this.child});

  @override
  Widget build(BuildContext context) {
    if (!EnvironmentVariables.enableDebugFeatures) {
      return child;
    }

    return Banner(
      message: EnvironmentVariables.flavor.name,
      location: BannerLocation.topEnd,
      child: child,
    );
  }
}
