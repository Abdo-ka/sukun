import 'package:core/core.dart';
import 'package:core/src/theme/extension_color_scheme.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:just_the_tooltip/just_the_tooltip.dart';

class AppTooltip extends StatelessWidget {
  final Widget child;
  final String title;
  final String? message;
  final EdgeInsetsGeometry? margin;
  final AxisDirection? preferredDirection;
  final bool preferredMediumTitle;

  ///TODO customize this
  const AppTooltip({
    this.preferredMediumTitle = false,
    super.key,
    required this.child,
    required this.title,
    this.message,
    this.margin,
    this.preferredDirection,
  });

  @override
  Widget build(BuildContext context) {
    return JustTheTooltip(
      preferredDirection:
          preferredDirection ?? AxisDirection.down,
      borderRadius: BorderRadius.circular(8.r),
      triggerMode: TooltipTriggerMode.tap,
      tailLength: 6.h,
      tailBaseWidth: 8.w,
      margin: margin ?? REdgeInsets.all(8.0),
      shadow: Shadow(
          color: context.colorScheme.systemGray.shade900
              .withValues(alpha: 0.03),
          blurRadius: 6.r,
          offset: const Offset(0, 12)),
      backgroundColor:
          context.colorScheme.systemGray.shade900,
      content: Padding(
        padding: message != null
            ? REdgeInsets.all(16.0)
            : REdgeInsets.symmetric(
                vertical: 8, horizontal: 12),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisSize: MainAxisSize.min,
          children: [
            AppText(
              title,
              color: context.colorScheme.surface,
              style: preferredMediumTitle
                  ? context.textTheme.labelMedium
                  : context.textTheme.labelMedium?.copyWith(
                      fontWeight: FontWeight.w600,
                    ),
            ),
            if (message != null) 4.verticalSpace,
            if (message != null)
              AppText(
                message!,
                color:
                    context.colorScheme.systemGray.shade300,
                style: context.textTheme.labelMedium,
              ),
          ],
        ),
      ),
      // message: message,
      // decoration: ShapeDecoration(
      //   color: Colors.blue,
      //   shape: ToolTipCustomShape(),
      // ),

      child: GestureDetector(
        behavior: HitTestBehavior.opaque,
        // onTap: () => _onTap(key),
        child: child,
      ),
    );
  }
}
