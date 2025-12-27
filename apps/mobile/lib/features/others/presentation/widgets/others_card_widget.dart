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
              //for a full width we defined an extension in context to get full width it's "context.width"
              width: double.infinity,
              decoration: BoxDecoration(
                color: context.colorScheme.surface,
                borderRadius: BorderRadius.circular(16),
                border: Border.all(
                  color: const Color(0xffECEEEF),
                  width: 2,
                ),
              ),
              child: Center(
                //don't need to check if it's svg you can just pass value of path to image and AppImage will define is it svg or not take a look on Constant file Line:3-9 that I defined inside it some images and take a look how I use it
                child: item.isSvg
                    ? AppImage.asset(
                        item.icon,
                        height: 60.h,
                      )
                    : item.icon.image(height: 60.h),
              ),
            ),
          ),
          8.verticalSpace,
          //follow design typography in text for example here you should use AppText.bodyLarge
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
