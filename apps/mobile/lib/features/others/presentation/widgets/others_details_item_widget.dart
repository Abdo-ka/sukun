import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/features/others/domain/entities/other_entity.dart';
import 'package:mobile/gen/assets.gen.dart';

class OthersDetailsItemWidget extends StatelessWidget {
  final OtherItemEntity item;
  final int index;

  const OthersDetailsItemWidget({
    super.key,
    required this.item,
    required this.index,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 62.h,
      decoration: BoxDecoration(
        //same here for colors
        color: index.isEven
            ? const Color(0xffF9F9F9)
            : Colors.transparent, // Alternating colors
        borderRadius: BorderRadius.circular(8),
      ),
      padding: const EdgeInsets.symmetric(horizontal: 16),
      child: Row(
        children: [
          // 1. Diamond (Rightmost)
          AppImage.asset(
            Assets.icons.dimond,
            width: 12.w,
            height: 12.h,
          ),
          8.horizontalSpace,

          // 2. Text
          Expanded(
            child: AppText.bodyMedium(
              item.title ?? '',
              textAlign: TextAlign.right,
            ),
          ),

          // 3. Star
          AppImage.asset(
            Assets.icons.star,
            width: 20.w,
            height: 20.h,
          ),
          8.horizontalSpace,

          // 4. Arrow (Leftmost)
          AppImage.asset(
            Assets.icons.arrowLeftSquare,
            width: 24.w,
            height: 24.h,
          ),
        ],
      ),
    );
  }
}
