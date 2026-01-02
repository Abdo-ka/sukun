import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';

import '../../core.dart';

class AppDropDown<T> extends StatelessWidget {
  final String label;
  final String? name;
  final bool enabled;
  final T? initialValue;
  final String? hint;
  final double? verticalMargin;
  final String? Function(T?)? validator;
  final List<DropdownMenuItem<T>>? items;
  final VoidCallback? onRetry;
  final void Function(T?)? onChanged;
  final Widget? icon;
  final Status? status;
  final double? width;
  final double? height;

  const AppDropDown({
    super.key,
    required this.label,
    required this.items,
    this.onRetry,
    this.enabled = true,
    this.validator,
    this.name,
    this.initialValue,
    this.hint,
    this.verticalMargin,
    this.onChanged,
    this.icon,
    this.status,
    this.width,
    this.height,
  });

  @override
  Widget build(BuildContext context) {
    final bool isLoading = status == Status.loading;
    final bool isFailure = status == Status.failure;

    return SizedBox(
      width: width,
      height: height,
      child: Padding(
        padding: verticalMargin != null
            ? EdgeInsets.symmetric(
                vertical: verticalMargin!,
              )
            : EdgeInsets.zero,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisSize: MainAxisSize.min,
          children: [
            if (label.isNotEmpty) ...[
              Padding(
                padding: REdgeInsetsDirectional.only(
                  start: 10,
                ),
                child: AppText(label),
              ),
              12.verticalSpace,
            ],
            FormBuilderDropdown<T>(
              name: name ?? label,
              items: isLoading || isFailure || items == null
                  ? []
                  : items!,
              enabled: isLoading ? false : enabled,
              validator: validator,
              onChanged: onChanged,
              initialValue: initialValue,
              decoration: InputDecoration(
                border: OutlineInputBorder(
                  borderSide: BorderSide(
                    color: context.colorScheme.outline,
                  ),
                  borderRadius: BorderRadius.circular(16),
                ),
                disabledBorder: OutlineInputBorder(
                  borderSide: BorderSide(
                    color: context.colorScheme.outline,
                  ),
                  borderRadius: BorderRadius.circular(16),
                ),
                errorBorder: OutlineInputBorder(
                  borderSide: BorderSide(
                    color: context.colorScheme.error,
                  ),
                  borderRadius: BorderRadius.circular(16),
                ),
                enabledBorder: OutlineInputBorder(
                  borderSide: BorderSide(
                    color: context.colorScheme.outline,
                  ),
                  borderRadius: BorderRadius.circular(16),
                ),
                focusedBorder: OutlineInputBorder(
                  borderSide: BorderSide(
                    color: context.colorScheme.primary,
                  ),
                  borderRadius: BorderRadius.circular(16),
                ),
                prefixIcon: icon,
                prefixIconConstraints: BoxConstraints(
                  maxHeight: 40.h,
                  minHeight: 10.h,
                  minWidth: 40.w,
                ),
                labelText: hint,
                hintStyle: context.textTheme.titleSmall
                    ?.copyWith(color: Colors.grey),
                suffixIconConstraints: BoxConstraints(
                  maxWidth: 80.w,
                  maxHeight: 30.h,
                ),
                suffixIcon: isLoading
                    ? Container(
                        margin: REdgeInsetsDirectional.only(
                          end: 10,
                        ),
                        width: 20.w,
                        height: 20.h,
                        child:
                            const CircularProgressIndicator(
                              strokeWidth: 2,
                            ),
                      )
                    : isFailure && onRetry != null
                    ? IconButton(
                        onPressed: onRetry,
                        icon: Icon(
                          Icons.repeat,
                          color:
                              context.colorScheme.primary,
                        ),
                      )
                    : SizedBox.shrink(),
                contentPadding: REdgeInsetsDirectional.only(
                  start: 5,
                  top: 10,
                  bottom: 10,
                  end: 20,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
