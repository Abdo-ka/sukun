import 'dart:io';

import 'package:flutter/material.dart';
import 'package:in_app_update/in_app_update.dart';
import 'package:upgrader/upgrader.dart';

class AppUpdateWrapper extends StatefulWidget {
  final Widget child;
  const AppUpdateWrapper({super.key, required this.child});

  @override
  State<AppUpdateWrapper> createState() =>
      _AppUpdateWrapperState();
}

class _AppUpdateWrapperState extends State<AppUpdateWrapper>
    with WidgetsBindingObserver {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addObserver(this);
    if (Platform.isAndroid) {
      _checkForAndroidUpdate();
    }
  }

  @override
  void dispose() {
    WidgetsBinding.instance.removeObserver(this);
    super.dispose();
  }

  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    super.didChangeAppLifecycleState(state);
    if (state == AppLifecycleState.resumed) {
      if (Platform.isAndroid) {
        _checkForAndroidUpdate();
      }
    }
  }

  Future<void> _checkForAndroidUpdate() async {
    try {
      final info = await InAppUpdate.checkForUpdate();
      if (info.updateAvailability ==
          UpdateAvailability.updateAvailable) {
        if (info.immediateUpdateAllowed) {
          await InAppUpdate.performImmediateUpdate();
        }
      } else if (info.updateAvailability ==
          UpdateAvailability
              .developerTriggeredUpdateInProgress) {
        await InAppUpdate.performImmediateUpdate();
      }
    } catch (e) {
      debugPrint("InAppUpdate error: $e");
    }
  }

  @override
  Widget build(BuildContext context) {
    if (Platform.isIOS) {
      return UpgradeAlert(
        dialogStyle: UpgradeDialogStyle.cupertino,
        showIgnore: false,
        showLater: false,
        child: widget.child,
      );
    }
    return widget.child;
  }
}
