import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/core/config/constant.dart';
import 'package:mobile/features/prayer/presentation/widgets/days_of_week_widget.dart';
import 'package:mobile/gen/assets.gen.dart';

@RoutePage()
class SettingPrayPage extends StatelessWidget {
  const SettingPrayPage({super.key});

  @override
  Widget build(BuildContext context) {
    return AppScaffold(
      appBar: AppAppBar(
        title: AppText.titleMedium('مواقيت الصلاة'),
        centerTitle: false,
      ),
      body: ListView(
        children: [
          DaysOfWeekWidget(),
          29.verticalSpace,
          SelectLocationWidget(),
          12.verticalSpace,
          SelectCalculateMethodWidget(),
          12.verticalSpace,
          SilentModeAfterPrayer(),
          12.verticalSpace,
          AlarmBeforeTenMinutesWidget(),
          12.verticalSpace,
          Padding(
            padding: const EdgeInsets.symmetric(
              horizontal: 16,
            ),
            child: Column(
              mainAxisAlignment: .start,
              crossAxisAlignment: .start,
              children: [
                AppText.titleMedium('الأذان'),
                12.verticalSpace,
                ListView.separated(
                  shrinkWrap: true,
                  physics: NeverScrollableScrollPhysics(),
                  itemBuilder:
                      (BuildContext context, int index) =>
                          PrayerTimeAlarmWidget(
                            item: prayerNames[index],
                          ),
                  separatorBuilder:
                      (BuildContext context, int index) =>
                          12.verticalSpace,
                  itemCount: prayerNames.length,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

class PrayerTimeAlarmWidget extends StatelessWidget {
  final Map<String, String> item;
  const PrayerTimeAlarmWidget({
    super.key,
    required this.item,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 50.h,
      decoration: BoxDecoration(
        color: context.colorScheme.surface,
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.2),
            blurRadius: 1,
            offset: const Offset(0, 1),
          ),
        ],
      ),
      child: Row(
        mainAxisAlignment: .spaceBetween,
        children: [
          Row(
            children: [
              AppImage.asset(item['image']!, size: 30),
              10.horizontalSpace,
              AppText.titleMedium(
                'وقت ${item['name']}',
                fontWeight: FontWeight.w400,
                color: context.colorScheme.onSurfaceVariant,
              ),
            ],
          ),
          SizedBox(
            width: 160.w,
            height: 50.h,
            child: AppDropDown(
              items: [
                DropdownMenuItem(
                  value: 'vibration',
                  child: AppText.bodyLarge('اهتزاز'),
                ),
                DropdownMenuItem(
                  value: 'takbir',
                  child: AppText.bodyLarge('تكبير'),
                ),
                DropdownMenuItem(
                  value: 'azan',
                  child: AppText.bodyLarge('اذان كامل'),
                ),
              ],
              label: '',
            ),
          ),
        ],
      ),
    );
  }
}

class AlarmBeforeTenMinutesWidget extends StatelessWidget {
  const AlarmBeforeTenMinutesWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 68.h,

      decoration: BoxDecoration(
        color: context.colorScheme.surface,
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.2),
            blurRadius: 1,
            offset: const Offset(0, 1),
          ),
        ],
      ),
      child: Row(
        children: [
          AppSwitch(value: false, onChanged: (value) {}),
          20.horizontalSpace,
          Flexible(
            child: AppText.bodyLarge(
              'تنبيه قبل الأذان بـ 10 دقائق',
              color: context.colorScheme.onSurfaceVariant,
              maxLines: 2,
            ),
          ),
        ],
      ),
    );
  }
}

class SilentModeAfterPrayer extends StatelessWidget {
  const SilentModeAfterPrayer({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 68.h,

      decoration: BoxDecoration(
        color: context.colorScheme.surface,
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.2),
            blurRadius: 1,
            offset: const Offset(0, 1),
          ),
        ],
      ),
      child: Row(
        children: [
          AppSwitch(value: false, onChanged: (value) {}),
          20.horizontalSpace,
          Flexible(
            child: AppText.bodyLarge(
              'ضبط الهاتف على وضع الصامت لمدة 15 دقيقة بعد الصلاة',
              color: context.colorScheme.onSurfaceVariant,
              maxLines: 2,
            ),
          ),
        ],
      ),
    );
  }
}

class SelectCalculateMethodWidget extends StatelessWidget {
  const SelectCalculateMethodWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 68.h,

      decoration: BoxDecoration(
        color: context.colorScheme.surface,
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.2),
            blurRadius: 1,
            offset: const Offset(0, 1),
          ),
        ],
      ),
      padding: EdgeInsets.symmetric(horizontal: 16),
      child: Row(
        mainAxisAlignment: .spaceBetween,
        children: [
          AppText.bodyLarge(
            'إعدادات المنطقة الزمنية الخاصة',
            color:
                context.colorScheme.surfaceContainerHighest,
          ),
          Row(
            children: [
              AppText(
                'قرية أم القرى',
                color: context.colorScheme.outline,
              ),
              AppImage.asset(Assets.icons.arrowLeftSquare),
            ],
          ),
        ],
      ),
    );
  }
}

class SelectLocationWidget extends StatelessWidget {
  const SelectLocationWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 68.h,

      decoration: BoxDecoration(
        color: context.colorScheme.surface,
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.2),
            blurRadius: 1,
            offset: const Offset(0, 1),
          ),
        ],
      ),
      padding: EdgeInsets.symmetric(horizontal: 16),
      child: Row(
        mainAxisAlignment: .spaceBetween,
        children: [
          AppText.bodyLarge(
            'تحديد الموقع الجغرافي بدقة',
            color:
                context.colorScheme.surfaceContainerHighest,
          ),
          Row(
            children: [
              AppText(
                'مدينة حلب، سوريا',
                color: context.colorScheme.outline,
              ),
              AppImage.asset(Assets.icons.arrowLeftSquare),
            ],
          ),
        ],
      ),
    );
  }
}
