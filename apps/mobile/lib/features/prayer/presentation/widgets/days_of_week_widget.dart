import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/core/config/constant.dart';

class DaysOfWeekWidget extends StatelessWidget {
  const DaysOfWeekWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 80.h,
      child: ListView.builder(
        scrollDirection: Axis.horizontal,
        itemCount: daysOfWeek.length,
        itemBuilder: (BuildContext context, int index) =>
            Padding(
              padding: EdgeInsets.symmetric(
                horizontal: 3,
                vertical: 2,
              ),
              child: ButtonWidget(
                width: 55.w,
                height: 70.h,
                foregroundColor:
                    context.colorScheme.outline,
                backgroundColor: index == 3
                    ? Color(
                        0xFF34937D,
                      ).withValues(alpha: .1)
                    : null,
                isOutlined: true,
                borderColor: index == 3
                    ? Color(0xFF34937D)
                    : null,
                prefixIcon: Column(
                  mainAxisSize: .min,
                  mainAxisAlignment: .center,
                  children: [
                    AppText.labelLarge(
                      daysOfWeek[index],
                      softWrap: true,
                      fontWeight: FontWeight.bold,
                      overflow: TextOverflow.ellipsis,
                      color: index == 3
                          ? Color(0xFF34937D)
                          : context.colorScheme.outline,
                    ),
                    AppText.bodyLarge(
                      color: index == 3
                          ? Color(0xFF34937D)
                          : null,
                      fontWeight: FontWeight.bold,
                      '${DateTime.now().add(Duration(days: index - 6)).day}',
                    ),
                  ],
                ),
                onPressed: () {},
              ),
            ),
      ),
    );
  }
}
