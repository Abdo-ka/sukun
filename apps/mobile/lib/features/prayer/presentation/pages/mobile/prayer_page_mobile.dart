import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/features/home/presentation/widgets/next_prayer_widget.dart';
import 'package:mobile/features/prayer/presentation/widgets/date_hijri_widget.dart';
import 'package:mobile/features/prayer/presentation/widgets/days_of_week_widget.dart';
import 'package:mobile/features/prayer/presentation/widgets/prayer_time_date_widget.dart';
import 'package:mobile/gen/assets.gen.dart';

class PrayerPageMobile extends StatelessWidget {
  const PrayerPageMobile({super.key});

  @override
  Widget build(BuildContext context) {
    return AppScaffold(
      appBar: AppAppBar(
        centerTitle: false,
        title: AppText.titleMedium('مواقيت الصلاة'),
        actions: [
          ButtonWidget(
            onPressed: () {},
            width: 38.w,
            height: 38.h,
            prefixIcon: AppImage.asset(
              Assets.icons.setting,
            ),
          ),
          8.horizontalSpace,
          ButtonWidget(
            text: 'سوريا , حلب',
            textStyle: context.textTheme.titleSmall
                ?.copyWith(
                  fontFamily: 'Almarai',
                  fontWeight: FontWeight.bold,
                ),
            spaceBetween: 15,
            onPressed: () {},
            width: 120.w,
            height: 38.h,
            prefixIcon: AppImage.asset(
              Assets.icons.location,
            ),
          ),
        ],
      ),
      body: ListView(
        children: [
          DateHijriWidget(),
          16.verticalSpace,
          DaysOfWeekWidget(),
          16.verticalSpace,
          Padding(
            padding: const EdgeInsets.symmetric(
              horizontal: 16,
            ),
            child: Column(
              children: [
                NextPrayerWidget(isFromHome: false),
                16.verticalSpace,
                PrayerTimeDateWidget(),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
