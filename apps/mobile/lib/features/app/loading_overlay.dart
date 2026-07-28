import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:loader_overlay/loader_overlay.dart';
import 'package:lottie/lottie.dart';
import 'package:mobile/gen/assets.gen.dart';

class LoadingOverlay extends StatelessWidget {
  final Widget child;

  const LoadingOverlay({super.key, required this.child});

  @override
  Widget build(BuildContext context) {
    return GlobalLoaderOverlay(
      useDefaultLoading: false,
      overlayColor: const Color.fromARGB(
        255,
        106,
        106,
        106,
      ).withValues(alpha: .4),
      overlayWidgetBuilder: (_) => Center(
        child: Lottie.asset(
          Assets.lottie.rose,
          width: 600.w,
          height: 600.h,
          fit: BoxFit.fill,
          repeat: true,
        ),
      ),
      child: child,
    );
  }
}
