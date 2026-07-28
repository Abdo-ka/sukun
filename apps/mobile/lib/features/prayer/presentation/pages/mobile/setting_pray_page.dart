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
            color: Colors.grey.withValues(alpha: 0.2),
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
            color: Colors.grey.withValues(alpha: 0.2),
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
            color: Colors.grey.withValues(alpha: 0.2),
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
            color: Colors.grey.withValues(alpha: 0.2),
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

class SelectLocationWidget extends StatefulWidget {
  const SelectLocationWidget({super.key});

  @override
  State<SelectLocationWidget> createState() =>
      _SelectLocationWidgetState();
}

class _SelectLocationWidgetState
    extends State<SelectLocationWidget> {
  Country? _selected;

  Future<void> _pick() async {
    final country = await showModalBottomSheet<Country>(
      context: context,
      isScrollControlled: true,
      builder: (_) => const _CountrySearchSheet(),
    );
    if (country == null) return;
    setState(() => _selected = country);
    // The bug was here: fetching used an index into the ORIGINAL list while
    // the user tapped a row in the FILTERED list, so it fetched another
    // country. `country` is the exact item the user tapped — fetch with it.
    // TODO(prayer): context.read<PrayerBloc>().add(FetchPrayerTimes(country));
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: _pick,
      child: Container(
        height: 68.h,

        decoration: BoxDecoration(
          color: context.colorScheme.surface,
          boxShadow: [
            BoxShadow(
              color: Colors.grey.withValues(alpha: 0.2),
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
              color: context
                  .colorScheme
                  .surfaceContainerHighest,
            ),
            Row(
              children: [
                AppText(
                  _selected?.name ?? 'مدينة حلب، سوريا',
                  color: context.colorScheme.outline,
                ),
                AppImage.asset(
                  Assets.icons.arrowLeftSquare,
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}

class _CountrySearchSheet extends StatefulWidget {
  const _CountrySearchSheet();

  @override
  State<_CountrySearchSheet> createState() =>
      _CountrySearchSheetState();
}

class _CountrySearchSheetState
    extends State<_CountrySearchSheet> {
  String _query = '';

  @override
  Widget build(BuildContext context) {
    // Filter the ORIGINAL source each build. We render `filtered` and select
    // `filtered[index]` — never `countries[index]` — so selection stays
    // correct after searching.
    final filtered = _query.isEmpty
        ? countries
        : countries
              .where(
                (c) => c.name.toLowerCase().contains(
                  _query.toLowerCase(),
                ),
              )
              .toList();

    return Padding(
      padding: EdgeInsets.only(
        bottom: MediaQuery.of(context).viewInsets.bottom,
      ),
      child: SizedBox(
        height: 500.h,
        child: Column(
          children: [
            Padding(
              padding: const EdgeInsets.all(16),
              child: TextField(
                autofocus: true,
                textDirection: TextDirection.rtl,
                decoration: InputDecoration(
                  hintText: 'ابحث عن دولة',
                  prefixIcon: const Icon(Icons.search),
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(16),
                  ),
                ),
                onChanged: (v) =>
                    setState(() => _query = v),
              ),
            ),
            Expanded(
              child: ListView.builder(
                itemCount: filtered.length,
                itemBuilder: (context, index) {
                  final country = filtered[index];
                  return ListTile(
                    leading: AppImage.asset(
                      'packages/core/assets/flags/${country.name}.svg',
                      size: 20,
                    ),
                    title: AppText.bodyMedium(
                      '${country.name} (${country.dialCode})',
                    ),
                    onTap: () =>
                        Navigator.pop(context, country),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
