import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

@RoutePage()
class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  static const notificationChannel = MethodChannel(
    'NotificationPlatform',
  );
  @override
  Widget build(BuildContext context) {
    return AppScaffold(
      appBar: AppAppBar(
        title: AppText.bodyLarge('text'),
        backgroundColor: context.colorScheme.primary,
      ),
      body: Center(child: AppText.labelLarge('test test')),
    );
  }
}
