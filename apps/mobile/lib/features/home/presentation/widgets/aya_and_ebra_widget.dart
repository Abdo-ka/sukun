import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';

class AyaAndEbraWidget extends StatelessWidget {
  const AyaAndEbraWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 150.h,
      width: context.width,
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(20),
        border: Border.all(
          color: context.colorScheme.surfaceContainer,
          width: 2,
        ),
      ),
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: .start,
          crossAxisAlignment: .start,
          children: [
            AppText.titleMedium('اية و عبرة'),
            12.verticalSpace,
            Center(
              child: AppText.headlineSmall(
                'وَنَحْنُ أَقْرَبُ إِلَيْهِ مِنْ حَبْلِ الْوَرِيدِ (16)',
              ),
            ),
            12.verticalSpace,
            Center(
              child: AppText.labelLarge(
                textAlign: TextAlign.center,
                'هو قرب ذوات الملائكة وقرب علم الله ؛ فذاتهم أقرب إلى قلب العبد من حبل الوريد',
                maxLines: 3,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
