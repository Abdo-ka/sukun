import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:core/src/theme/extension_color_scheme.dart';

/// Optimized checkbox with smooth animations and cached computations
class AppCheckbox extends StatelessWidget {
  const AppCheckbox({
    super.key,
    this.isSelected = false,
    this.onChanged,
    this.height,
    this.width,
  });

  final bool isSelected;
  final ValueChanged<bool>? onChanged;
  final double? height;
  final double? width;

  @override
  Widget build(BuildContext context) {
    final isDisabled = onChanged == null;

    return SizedBox(
      height: height,
      width: width,
      child: InkWell(
        onTap: () {
          onChanged?.call(!isSelected);
        },
        child: Center(
          child: AnimatedContainer(
            duration: const Duration(milliseconds: 200),
            height: 20.r,
            width: 20.r,
            decoration: BoxDecoration(
              border: buildBorder(),
              borderRadius: BorderRadius.circular(6.r),
              color: backgroundColor(context),
              boxShadow: isSelected && !isDisabled
                  ? [
                      BoxShadow(
                        blurRadius: 3,
                        spreadRadius: 0,
                        offset: const Offset(0, 1),
                        color: _shadowCard1(0.05),
                      )
                    ]
                  : null,
            ),
            child: Center(
                child: Icon(
              Icons.check,
              color: iconColor(context),
              size: 16.r,
            )),
          ),
        ),
      ),
    );
  }

  Color iconColor(BuildContext context) {
    if (!isSelected) return Colors.transparent;
    if (onChanged == null) return const Color(0xffD0D5DD);
    return const Color(0xffE24D06);
  }

  Border? buildBorder() {
    if (isSelected) return null;
    return Border.all(
      color: const Color(0xffEAECF0),
      width: 1.25.r,
    );
  }

  Color backgroundColor(BuildContext context) {
    final isDisabled = onChanged == null;
    if (isDisabled) {
      return isSelected ? const Color(0xffEAECF0) : const Color(0xffF2F4F7);
    }
    return isSelected
        ? context.colorScheme.brandPrimary.shade50
        : context.colorScheme.surface;
  }
}

/// Shadow color for checkbox with opacity
Color _shadowCard1(double opacity) => Color.fromRGBO(16, 24, 40, opacity);
