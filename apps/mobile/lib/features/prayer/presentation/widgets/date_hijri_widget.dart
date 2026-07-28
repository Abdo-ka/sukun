import 'package:core/core.dart';
import 'package:flutter/material.dart';

class DateHijriWidget extends StatelessWidget {
  const DateHijriWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.all(16),
      child: Row(
        mainAxisAlignment: .spaceBetween,
        children: [
          AppText.bodyMedium('22 حزيران 2023'),
          AppText.bodyMedium('3 ذي الحجة 1444'),
        ],
      ),
    );
  }
}
