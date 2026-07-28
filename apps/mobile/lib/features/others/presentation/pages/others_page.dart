import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/core/di/di_container.dart';
import 'package:mobile/features/others/presentation/pages/mobile/others_page_mobile.dart';
import 'package:mobile/features/others/presentation/state/bloc/others_cubit.dart';

@RoutePage()
class OthersPage extends StatelessWidget {
  const OthersPage({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => getIt<OthersCubit>()..getCategories(),
      child: AppScaffold(
        body: PageLayoutBuilder(
          mobile: (context) => OthersPageMobile(),
        ),
      ),
    );
  }
}
