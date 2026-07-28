import 'package:core/core.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:intl/intl.dart';

class AppDatePicker extends StatelessWidget {
  final String name;
  final EdgeInsets? margin;
  final String? title;
  final double elevation;
  final TextEditingController _controller = TextEditingController();
  AppDatePicker({
    super.key,
    required this.name,
    this.margin,
    this.title,
    this.elevation = 2,
  });

  void _showDatePicker(BuildContext context) {
    showCupertinoModalPopup(
      context: context,
      builder: (_) => Container(
        height: 270.h,
        color: const Color.fromARGB(255, 255, 255, 255),
        child: Column(
          children: [
            const Spacer(flex: 2),
            SizedBox(
              height: 160,
              child: CupertinoDatePicker(
                mode: CupertinoDatePickerMode.date,
                onDateTimeChanged: (val) {
                  _controller.text = DateFormat('yyyy-MM-dd').format(val);
                },
              ),
            ),
            const Spacer(),
            // Close the modal
            CupertinoButton(
                child: const Text('OK'),
                onPressed: () => Navigator.of(context).pop()),
            const Spacer(),
          ],
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () => _showDatePicker(context),
      child: AppTextFormField(
        enabled: false,
        title: title,
        elevation: elevation,
        margin: margin,
        name: name,
        controller: _controller,
        hintText: 'YYYY-MM-DD',
        suffixIcon: Padding(
          padding: REdgeInsets.symmetric(vertical: 14.w),
          child: AppImage.asset('packages/core/assets/icons/calendar.svg',
              size: 15.w, height: 15.w, width: 15.w),
        ),
      ),
    );
  }
}
