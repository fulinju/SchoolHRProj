package sl.hr_client.main.setting;

import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import java.io.File;

import sl.base.utils.UtilsVersion;
import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;
import sl.hr_client.imp.FragmentBackListener;
import sl.hr_client.main.MainActivity;
import sl.hr_client.utils.constant.ConstantData;

/**
 * Created by Administrator on 2017/5/3.
 */

public class AboutFragment extends BaseFragment implements FragmentBackListener {
    public static final String ABOUT = "About";

    private View aboutView;
    private Context ctx;

    private TextView tvTitle;
    private TextView tvBack;

    private TextView tvAbout;
    private TextView tvDownloadPath;
    private TextView tvContact;
//    private Button btnOpen;


    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        tvTitle = (TextView) aboutView.findViewById(R.id.tv_head);
        tvBack = (TextView) aboutView.findViewById(R.id.tv_head_right);
        tvAbout = (TextView) aboutView.findViewById(R.id.tv_about);
        tvDownloadPath = (TextView) aboutView.findViewById(R.id.tv_download_path);
        tvContact = (TextView) aboutView.findViewById(R.id.tv_contact);
//        btnOpen = (Button) aboutView.findViewById(R.id.btn_open);

        tvTitle.setText(getString(R.string.about));
        tvBack.setText(getString(R.string.back));

        tvBack.setVisibility(View.VISIBLE);

        tvAbout.setText(getString(R.string.version) + UtilsVersion.getVersion(ctx));
        tvDownloadPath.setText(getString(R.string.download_path) + ConstantData.downloadPath);
        tvContact.setText(getString(R.string.contact_method) + "\n"
                + getString(R.string.phone) + ":15196606812" + "\n"
                + getString(R.string.mailbox) + ":ccc.surpass@qq.com");
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

//        btnOpen.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                openAssignFolder(ConstantData.downloadPath);
//            }
//        });
    }

    private void funcBack() {
        getFragmentManager().popBackStack();
    }


//    private void openAssignFolder(String path) {
//        File file = new File(path);
//        if (null == file || !file.exists()) {
//            return;
//        }
//        Intent intent = new Intent(Intent.ACTION_GET_CONTENT);
//        intent.addCategory(Intent.CATEGORY_DEFAULT);
//        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
//        intent.setDataAndType(Uri.fromFile(file), "*/*");
//
////        File parentFlie = new File(file.getParent());
////        Intent intent = new Intent(Intent.ACTION_GET_CONTENT);
////        intent.setDataAndType(Uri.fromFile(parentFlie), "*/*");
////        intent.addCategory(Intent.CATEGORY_OPENABLE);
//        try {
//            startActivity(intent);
////            startActivity(Intent.createChooser(intent, "选择浏览工具"));
//        } catch (ActivityNotFoundException e) {
//            e.printStackTrace();
//        }
//    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);

        if (context instanceof MainActivity) {
            ((MainActivity) context).setBackListener(this);
            ((MainActivity) context).setInterception(true);
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        if (getActivity() instanceof MainActivity) {
            ((MainActivity) getActivity()).setBackListener(null);
            ((MainActivity) getActivity()).setInterception(false);
        }
    }

    @Override
    public void onBackForward() {
        funcBack();
    }
}
