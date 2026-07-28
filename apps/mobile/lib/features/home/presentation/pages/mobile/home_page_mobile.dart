import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/core/config/constant.dart';
import 'package:mobile/features/home/presentation/widgets/aya_and_ebra_widget.dart';
import 'package:mobile/features/home/presentation/widgets/next_prayer_widget.dart';
import 'package:mobile/gen/assets.gen.dart';
import 'package:mobile/services/router/router.gr.dart';

@RoutePage()
class HomePageMobile extends StatefulWidget {
  const HomePageMobile({super.key});

  @override
  State<HomePageMobile> createState() =>
      _HomePageMobileState();
}

class _HomePageMobileState extends State<HomePageMobile> {
  @override
  Widget build(BuildContext context) {
    return AppScaffold(
      appBar: AppAppBar(
        actions: [
          ButtonWidget(
            width: 38.w,
            height: 38.h,
            onPressed: () {},
            prefixIcon: AppImage.asset(
              Assets.icons.notification,
              width: 25.w,
              size: 25,
              height: 25.h,
            ),
          ),
        ],
        leading: AppText.titleMedium(
          'سُكون',
          fontWeight: FontWeight.bold,
        ),
      ),
      body: ListView(
        shrinkWrap: true,
        padding: EdgeInsets.all(16),
        children: [
          NextPrayerWidget(),
          16.verticalSpace,
          AyaAndEbraWidget(),
          16.verticalSpace,
          GridView(
            shrinkWrap: true,
            physics: NeverScrollableScrollPhysics(),
            gridDelegate:
                SliverGridDelegateWithFixedCrossAxisCount(
                  crossAxisCount: 3,
                  childAspectRatio: 124.w / 153.h,
                  mainAxisSpacing: 12.w,
                  crossAxisSpacing: 12.h,
                ),
            children: List.generate(
              homeItemsGridView.length,
              (index) => Column(
                children: [
                  GestureDetector(
                    onTap: () {
                      if (homeItemsGridView[index]['label'] ==
                          'متفرقات') {
                        context.pushRoute(
                          const OthersRoute(),
                        );
                      }
                    },
                    child: Container(
                      width: 124.w,
                      height: 101.h,
                      decoration: BoxDecoration(
                        color: context.colorScheme.surface,
                        borderRadius: BorderRadius.circular(
                          16,
                        ),
                        border: Border.all(
                          color: context
                              .colorScheme
                              .surfaceContainer,
                        ),
                      ),
                      child: Padding(
                        padding: const EdgeInsets.symmetric(
                          horizontal: 24,
                          vertical: 12,
                        ),
                        child: AppImage.asset(
                          homeItemsGridView[index]['icon']!,
                          height: 76.h,
                          width: 76.w,
                        ),
                      ),
                    ),
                  ),
                  10.verticalSpace,
                  AppText.bodyMedium(
                    homeItemsGridView[index]['label']!,
                    textAlign: TextAlign.center,
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
