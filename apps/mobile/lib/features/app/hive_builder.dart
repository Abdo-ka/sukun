import 'package:flutter/material.dart';
import 'package:hive_flutter/hive_flutter.dart';
import 'package:mobile/services/hive_service.dart';

class HiveBuilder extends StatelessWidget {
  final Widget child;

  const HiveBuilder({super.key, required this.child});

  @override
  Widget build(BuildContext context) {
    return ValueListenableBuilder(
      valueListenable: HiveService.hive.listenable(),
      builder: (context, value, _) => MediaQuery(
        data: MediaQuery.of(context).copyWith(
          textScaler: const TextScaler.linear(1.0),
        ),
        child: child,
      ),
    );
  }
}
