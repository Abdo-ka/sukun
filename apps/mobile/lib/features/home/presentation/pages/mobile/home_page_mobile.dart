import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:mobile/features/home/presentation/widgets/next_prayer_widget.dart';
import 'package:mobile/gen/assets.gen.dart';

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
            onPressed: () {},
            prefixIcon: AppImage.asset(
              Assets.icons.notification,
            ),
          ),
        ],
        leading: AppText.titleMedium(
          'سُكون',
          fontWeight: FontWeight.bold,
        ),
      ),
      body: ListView(
        padding: EdgeInsets.all(16),
        children: [NextPrayerWidget()],
      ),
    );
  }
}
