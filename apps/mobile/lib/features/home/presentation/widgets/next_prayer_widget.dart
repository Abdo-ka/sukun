import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/gen/assets.gen.dart';
import 'package:mobile/services/router/router.gr.dart';

class NextPrayerWidget extends StatelessWidget {
  final bool? isFromHome;
  const NextPrayerWidget({
    super.key,
    this.isFromHome = true,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        isFromHome == true
            ? context.router.push(PrayerRoute())
            : null;
      },
      child: Container(
        height: 140.h,
        decoration: BoxDecoration(
          border: Border.all(color: Color(0xFF34937D)),
          borderRadius: BorderRadius.circular(20),
        ),
        child: Stack(
          alignment: Alignment.centerRight,
          children: [
            Container(
              height: context.height,
              width: 120,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.only(
                  topRight: Radius.circular(20),
                  bottomRight: Radius.circular(20),
                ),
                color:
                    context.colorScheme.secondaryContainer,
              ),
            ),

            Container(
              child: Padding(
                padding: EdgeInsetsGeometry.symmetric(
                  horizontal: 12,
                  vertical: 10,
                ),
                child: Row(
                  mainAxisAlignment: .spaceBetween,
                  children: [
                    Column(
                      mainAxisAlignment:
                          MainAxisAlignment.center,
                      crossAxisAlignment:
                          CrossAxisAlignment.start,
                      children: [
                        Row(
                          children: [
                            AppText.headlineSmall(
                              'الظهر',
                              color: context
                                  .colorScheme
                                  .secondary,
                              fontWeight: FontWeight.w800,
                            ),
                            8.horizontalSpace,
                            AppText.bodyMedium(
                              '03:40 مساءاً',
                              color: context
                                  .colorScheme
                                  .outline,
                            ),
                          ],
                        ),
                        5.verticalSpace,
                        Row(
                          children: [
                            AppText.headlineMedium(
                              '00:42:12',
                            ),
                            4.horizontalSpace,
                            AppText.bodyMedium(
                              'باقي',
                              color: context
                                  .colorScheme
                                  .outline,
                            ),
                          ],
                        ),
                        isFromHome == true
                            ? Column(
                                mainAxisAlignment: .start,
                                crossAxisAlignment: .start,
                                children: [
                                  5.verticalSpace,
                                  AppText.bodyMedium(
                                    'الاربعاء 3 ذي الحجة 1444',
                                    color: context
                                        .colorScheme
                                        .outline,
                                  ),
                                  AppText.bodyMedium(
                                    '22 حزيران 2023',
                                    color: context
                                        .colorScheme
                                        .outline,
                                  ),
                                ],
                              )
                            : SizedBox.shrink(),
                      ],
                    ),
                    AppImage.asset(Assets.icons.azanDuher),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
