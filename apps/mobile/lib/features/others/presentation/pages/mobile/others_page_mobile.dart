import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/core/di/di_container.dart';
import 'package:mobile/features/others/presentation/state/bloc/others_bloc.dart';
import 'package:mobile/features/others/presentation/widgets/others_card_widget.dart';
import 'package:mobile/features/others/presentation/widgets/others_wide_card_widget.dart';

class OthersPageMobile extends StatelessWidget {
  const OthersPageMobile({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) =>
          getIt<OthersBloc>()..add(const GetOthersCategoriesEvent()),
      child: const OthersView(),
    );
  }
}

class OthersView extends StatelessWidget {
  const OthersView({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        AppAppBar(
          title: AppText.titleMedium('متفرقات'),
          centerTitle: false,
        ),
        Expanded(
          child: BlocBuilder<OthersBloc, OthersState>(
            builder: (context, state) {
              if (state.status == const BlocStatus.loading()) {
                return const Center(child: CircularProgressIndicator());
              }
              if (state.categories.isEmpty) {
                return const SizedBox();
              }

              final gridItems =
                  state.categories.where((e) => e.id != 7).toList();
              final wideItem = state.categories.firstWhere((e) => e.id == 7,
                  orElse: () => state.categories.last);

              return ListView(
                padding: const EdgeInsets.all(16),
                children: [
                  GridView.builder(
                    shrinkWrap: true,
                    physics: const NeverScrollableScrollPhysics(),
                    gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                      crossAxisCount: 2,
                      childAspectRatio: 193 / 146,
                      crossAxisSpacing: 12.w,
                      mainAxisSpacing: 12.h,
                    ),
                    itemCount: gridItems.length,
                    itemBuilder: (context, index) {
                      return OthersCardWidget(item: gridItems[index]);
                    },
                  ),
                  12.verticalSpace,
                  OthersWideCardWidget(item: wideItem),
                ],
              );
            },
          ),
        ),
      ],
    );
  }
}
