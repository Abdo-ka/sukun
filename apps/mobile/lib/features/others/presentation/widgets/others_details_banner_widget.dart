import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/gen/assets.gen.dart';

class OthersDetailsBannerWidget extends StatelessWidget {
  final String title;
  final dynamic icon;

  const OthersDetailsBannerWidget({
    super.key,
    required this.title,
    required this.icon,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 144.h,
      width: double.infinity,
      decoration: BoxDecoration(
        color: context.colorScheme.surface,
        borderRadius: BorderRadius.circular(16),
        border: Border.all(
          // Fixed: Using theme color
          color: context.colorScheme.surfaceContainer,
          width: 2,
        ),
      ),
      child: Padding(
        padding: const EdgeInsets.symmetric(
          horizontal: 16.0,
        ),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                AppText.titleMedium(title),
                4.verticalSpace,
                // Fixed: Using theme color context.colorScheme.outline
                AppText.bodySmall(
                  'النساء الذي خلد الاسلام ذكرهم',
                  color: context.colorScheme.outline,
                ),
              ],
            ),
            // Fixed: Check icon type at runtime
            icon is String
                ? AppImage.asset(icon, height: 60.h)
                : (icon as AssetGenImage).image(height: 60.h),
          ],
        ),
      ),
    );
  }
}
