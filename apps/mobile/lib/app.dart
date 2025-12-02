import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/features/app/dismiss_keyboard_gesture_detector_wrapper.dart';
import 'package:mobile/features/app/env_banner.dart';
import 'package:mobile/features/app/hive_builder.dart';
import 'package:mobile/features/app/loading_overlay.dart';
import 'package:mobile/features/app/shadows_material_app.dart';

import 'services/localization/localization_services.dart';

class MobileApp extends StatelessWidget {
  const MobileApp({super.key});

  @override
  Widget build(BuildContext context) {
    return LocalizationServices(
      child: ScreenUtilInit(
        designSize: const Size(390, 844),
        minTextAdapt: false,
        splitScreenMode: true,
        builder: (context, child) => const LoadingOverlay(
          child: DismissKeyboardGestureDetectorWrapper(
            child: HiveBuilder(
              child: EnvBanner(child: MobileMaterialApp()),
            ),
          ),
        ),
      ),
    );
  }
}
