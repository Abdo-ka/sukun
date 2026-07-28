import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/features/others/domain/entities/other_entity.dart';
import 'package:mobile/gen/assets.gen.dart';
import 'package:mobile/services/router/router.gr.dart';

class OthersCardWidget extends StatelessWidget {
  final OtherCategoryEntity item;

  const OthersCardWidget({super.key, required this.item});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        if (item.id == 4) {
          context.pushRoute(
            OthersDetailsRoute(
              categoryId: item.id,
              title: item.title,
              icon: Assets.icons.hijab,
            ),
          );
        }
      },
      child: Column(
        children: [
          Expanded(
            child: Container(
              // Fixed: Using context.width instead of double.infinity
              width: context.width,
              decoration: BoxDecoration(
                color: context.colorScheme.surface,
                borderRadius: BorderRadius.circular(16),
                border: Border.all(
                  // Fixed: Using color from theme
                  color: context.colorScheme.surfaceContainer,
                  width: 2,
                ),
              ),
              child: Center(
                // Fixed: Check icon type at runtime instead of using isSvg flag
                child: item.icon is String
                    ? AppImage.asset(item.icon, height: 60.h)
                    : item.icon.image(height: 60.h),
              ),
            ),
          ),
          8.verticalSpace,
          // Fixed: Updated typography to bodyLarge
          AppText.bodyLarge(
            item.title,
            maxLines: 1,
            overflow: TextOverflow.ellipsis,
          ),
        ],
      ),
    );
  }
}
