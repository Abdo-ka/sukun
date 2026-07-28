// 🌎 Project imports:
import 'package:mobile/core/config/common/enum/enums.dart';

abstract class EnvironmentVariables {
  static const appName = 'mobile';

  // Runtime flavor configuration
  static Flavor _flavor = Flavor.Prod;
  static Flavor get flavor => _flavor;

  // Base API URLs for different environments
  static const String _devBaseUrl =
      'https://dev-api.mobile.com';
  static const String _stagingBaseUrl =
      'https://staging-api.mobile.com';
  static const String _prodBaseUrl =
      'https://api.mobile.com';
  static const String sentryDsn =
      "https://39939b0726506e4767c53d22da8271c6@o4509753626001408.ingest.us.sentry.io/4510403993731072";
  // Get the appropriate base URL based on flavor
  static String get baseUrl {
    switch (_flavor) {
      case Flavor.Dev:
        return _devBaseUrl;
      case Flavor.Stag:
        return _stagingBaseUrl;
      case Flavor.Prod:
        return _prodBaseUrl;
    }
  }

  // Configure environment at app startup
  static void configure(Flavor flavor) {
    _flavor = flavor;
  }

  // Check if debug features should be enabled
  static bool get enableDebugFeatures =>
      _flavor.isDev || _flavor.isStag;

  // App name with environment suffix
  static String get appNameWithEnv {
    switch (_flavor) {
      case Flavor.Dev:
        return '$appName (Dev)';
      case Flavor.Stag:
        return '$appName (Staging)';
      case Flavor.Prod:
        return appName;
    }
  }
}
