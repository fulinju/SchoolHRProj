package sl.hr_client.main.setting;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import sl.base.utils.UtilsVersion;
import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;
import sl.hr_client.utils.constant.ConstantData;

/**
 * Created by Administrator on 2017/5/3.
 */

public class AboutFragment extends BaseFragment {
    public static final String ABOUT = "About";

    private View aboutView;
    private Context ctx;

    private TextView tvTitle;
    private TextView tvBack;

    private TextView tvAbout;

    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        tvTitle = (TextView) aboutView.findViewById(R.id.tv_head);
        tvBack = (TextView) aboutView.findViewById(R.id.tv_head_right);
        tvAbout = (TextView) aboutView.findViewById(R.id.tv_about);

        tvTitle.setText(getString(R.string.about));
        tvBack.setText(getString(R.string.back));

        tvBack.setVisibility(View.VISIBLE);

        tvAbout.setText(getString(R.string.version) + UtilsVersion.getVersion(ctx)
                + "\n\n" + "文件下载路径：" + ConstantData.downloadPath);

        addListener();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        aboutView = inflater.inflate(R.layout.fragment_about, container, false);

        ctx = aboutView.getContext();
        return aboutView;
    }

    private void addListener() {
        tvBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                funcBack();
            }


        });
    }

    private void funcBack() {
        getFragmentManager().popBackStack();
    }

}
