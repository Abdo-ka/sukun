import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/core/config/constant.dart';

class PrayerTimeDateWidget extends StatelessWidget {
  const PrayerTimeDateWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: .center,
      crossAxisAlignment: .center,
      children: List.generate(
        prayerNames.length,
        (index) => Padding(
          padding: const EdgeInsets.symmetric(vertical: 6),
          child: Container(
            padding: EdgeInsets.symmetric(horizontal: 12),
            height: 62.h,
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(20),
              color: context.colorScheme.surfaceBright,
            ),
            child: Row(
              mainAxisAlignment: .spaceBetween,
              children: [
                Row(
                  children: [
                    AppImage.asset(
                      prayerNames[index]['image']!,
                    ),
                    12.horizontalSpace,
                    AppText.bodyLarge(
                      prayerNames[index]['name']!,
                      color: context.colorScheme.outline,
                    ),
                  ],
                ),
                AppText.bodyMedium('03:45 صباحاً'),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
