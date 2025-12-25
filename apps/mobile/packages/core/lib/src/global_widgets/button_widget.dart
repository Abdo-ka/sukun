import 'package:core/core.dart';
import 'package:flutter/material.dart';

class ButtonWidget extends StatelessWidget {
  const ButtonWidget({
    super.key,
    required this.onPressed,
    this.backgroundColor,
    this.foregroundColor,
    this.text,
    this.prefixIcon,
    this.suffixIcon,
    this.radius,
    this.isOutlined = false,
    this.isExpandedText = false,
    this.isElevated = false,
    this.borderColor,
    this.width,
    this.height,
    this.padding,
    this.spaceBetween,
    this.textDirection,
    this.textStyle,
    this.isDisabled = false,
    this.childPadding,
  });

  final Function() onPressed;
  final Color? backgroundColor;
  final Color? foregroundColor;
  final Color? borderColor;
  final String? text;
  final Widget? prefixIcon;
  final Widget? suffixIcon;
  final double? radius;
  final double? spaceBetween;
  final double? width;
  final double? height;
  final EdgeInsetsGeometry? padding;
  final EdgeInsetsGeometry? childPadding;
  final bool isOutlined;
  final bool isExpandedText;
  final bool isElevated;
  final TextDirection? textDirection;
  final TextStyle? textStyle;
  final bool isDisabled;

  @override
  Widget build(BuildContext context) {
    return ElevatedButton(
      onPressed: !isDisabled ? onPressed : () {},
      style: ElevatedButton.styleFrom(
        elevation: isElevated ? null : 0,
        backgroundColor: isOutlined
            ? (backgroundColor ?? Colors.transparent)
            : (backgroundColor ??
                  context.colorScheme.onInverseSurface),
        shadowColor: Colors.transparent,
        foregroundColor: foregroundColor,
        fixedSize: getFixedSize,
        minimumSize: const Size(0, 0),
        maximumSize: getFixedSize,
        padding: padding ?? EdgeInsets.zero,
        shape: ContinuousRectangleBorder(
          borderRadius: BorderRadius.circular(radius ?? 24),
          side: BorderSide(
            color: isOutlined
                ? (borderColor ??
                      Theme.of(context).colorScheme.outline)
                : Colors.transparent,
            width: 1,
          ),
        ),
      ),
      child: Row(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          if (prefixIcon != null) prefixIcon!,
          if (spaceBetween != null &&
              text != null &&
              prefixIcon != null)
            spaceBetween!.ws,
          if (text != null)
            AppText(
              text!,
              textDirection: textDirection,
              style: textStyle,
            ).expand(flex: isExpandedText ? 1 : 0),
          if (spaceBetween != null &&
              text != null &&
              suffixIcon != null)
            SizedBox(width: spaceBetween),
          if (suffixIcon != null) suffixIcon!,
        ],
      ).wrapPadding(childPadding ?? EdgeInsets.zero),
    );
  }

  Size? get getFixedSize {
    if (width == null && height == null) {
      return null;
    } else if (width != null && height == null) {
      return Size.fromWidth(width!);
    } else if (width == null && height != null) {
      return Size.fromHeight(height!);
    } else {
      return Size(width!, height!);
    }
  }
}
