import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';

class AppAppBar extends StatelessWidget
    implements PreferredSizeWidget {
  const AppAppBar({
    super.key,
    this.titleTextStyle,
    this.backgroundColor,
    this.shape,
    this.leading,
    this.title,
    this.bottom,
    this.elevation,
    this.clipBehavior,
    this.foregroundColor,
    this.surfaceTintColor = Colors.transparent,
    this.shadowColor,
    this.primary,
    this.titleSpacing,
    this.centerTitle,
    this.toolbarHeight,
    this.actions,
    this.actionsIconTheme,
    this.automaticallyImplyLeading,
    this.bottomOpacity,
    this.excludeHeaderSemantics,
    this.flexibleSpace,
    this.forceMaterialTransparency,
    this.iconTheme,
    this.leadingWidth,
    this.notificationPredicate,
    this.scrolledUnderElevation,
    this.systemOverlayStyle,
    this.toolbarOpacity,
    this.toolbarTextStyle,
    this.label,
    this.showDefaultBackButton = true,
    this.defaultBackButtonColor,
    this.labelColor = const Color(0xff667085),
  });

  final TextStyle? titleTextStyle;

  final Color? backgroundColor;

  final ShapeBorder? shape;

  final Widget? leading;

  final Widget? title;

  final PreferredSize? bottom;

  final double? elevation;

  final Clip? clipBehavior;

  final Color? foregroundColor;

  final Color? surfaceTintColor;

  final Color? shadowColor;

  final bool? primary;

  final double? titleSpacing;

  final bool? centerTitle;

  final double? toolbarHeight;

  final List<Widget>? actions;

  final IconThemeData? actionsIconTheme;

  final bool? automaticallyImplyLeading;

  final double? bottomOpacity;

  final bool? excludeHeaderSemantics;

  final Widget? flexibleSpace;

  final bool? forceMaterialTransparency;

  final IconThemeData? iconTheme;

  final double? leadingWidth;

  final ScrollNotificationPredicate? notificationPredicate;

  final double? scrolledUnderElevation;

  final SystemUiOverlayStyle? systemOverlayStyle;

  final double? toolbarOpacity;

  final TextStyle? toolbarTextStyle;

  final String? label;

  final bool showDefaultBackButton;

  final Color? defaultBackButtonColor;

  final Color labelColor;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16),
      child: AppBar(
        titleTextStyle: titleTextStyle,
        backgroundColor:
            backgroundColor ?? context.colorScheme.surface,
        shape: shape,
        leading:
            leading ??
            (showDefaultBackButton
                ? context.locale.languageCode == 'ar'
                      ? Transform.scale(
                          scale: .8,
                          child: ButtonWidget(
                            width: 20.w,
                            height: 20.h,
                            prefixIcon: AppImage.asset(
                              'packages/core/assets/icons/arrow-right-square.svg',
                              width: 30.w,
                              height: 30.h,
                              size: 30,
                              color: defaultBackButtonColor,
                            ),
                            onPressed: () {
                              Navigator.pop(context);
                            },
                          ),
                        )
                      : ButtonWidget(
                          width: 38.w,
                          height: 38.h,
                          prefixIcon: Transform.rotate(
                            angle: 3.1416,
                            child: AppImage.asset(
                              'packages/core/assets/icons/arrow-right-square.svg',
                              width: 30.w,
                              height: 30.h,
                              size: 30,
                              color: defaultBackButtonColor,
                            ),
                          ),
                          onPressed: () {
                            Navigator.pop(context);
                          },
                        )
                : SizedBox.shrink()),
        title: label != null
            ? AppText.labelMedium(
                label!,
                color: titleTextStyle == null
                    ? labelColor
                    : null,
                style: titleTextStyle,
              )
            : title,
        key: key,
        bottom: bottom,
        elevation: elevation,
        clipBehavior: clipBehavior,
        foregroundColor: foregroundColor,
        surfaceTintColor: surfaceTintColor,
        shadowColor: shadowColor,
        primary: primary ?? true,
        titleSpacing: titleSpacing,
        centerTitle: centerTitle,
        toolbarHeight: toolbarHeight,
        actions: actions,
        actionsIconTheme: actionsIconTheme,
        automaticallyImplyLeading:
            automaticallyImplyLeading ?? true,
        bottomOpacity: bottomOpacity ?? 1,
        excludeHeaderSemantics:
            excludeHeaderSemantics ?? false,
        flexibleSpace: flexibleSpace,
        forceMaterialTransparency:
            forceMaterialTransparency ?? false,
        iconTheme: iconTheme,
        leadingWidth: leadingWidth,
        notificationPredicate:
            notificationPredicate ??
            defaultScrollNotificationPredicate,
        scrolledUnderElevation: scrolledUnderElevation,
        systemOverlayStyle: systemOverlayStyle,
        toolbarOpacity: toolbarOpacity ?? 1.0,
        toolbarTextStyle: toolbarTextStyle,
      ),
    );
  }

  @override
  Size get preferredSize => Size.fromHeight(
    (toolbarHeight ?? kToolbarHeight) +
        (bottom?.preferredSize.height ?? 0),
  );
}
