import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/features/others/domain/entities/other_entity.dart';
import 'package:mobile/services/router/router.gr.dart';

import 'package:mobile/gen/assets.gen.dart';

class OthersWideCardWidget extends StatelessWidget {
  final OtherCategoryEntity item;

  const OthersWideCardWidget({super.key, required this.item});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        if (item.id == 4) {
          context.pushRoute(OthersDetailsRoute(
              categoryId: item.id,
              title: item.title,
              icon: Assets.icons.hijab));
        }
      },
      child: Column(
        children: [
          Container(
            height: 101.h,
            width: 398.w,
            decoration: BoxDecoration(
              color: context.colorScheme.surface,
              borderRadius: BorderRadius.circular(16),
              border: Border.all(
                color: const Color(0xffECEEEF),
                width: 2,
              ),
            ),
            child: Center(
              child: item.isSvg
                  ? AppImage.asset(item.icon, height: 60.h)
                  : item.icon.image(height: 60.h),
            ),
          ),
          8.verticalSpace,
          AppText.bodyMedium(item.title),
        ],
      ),
    );
  }
}
