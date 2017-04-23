package sl.hr_client.main.link;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import sl.hr_client.R;
import sl.hr_client.base.ContentFragment;

/**
 * Created by Administrator on 2017/4/22.
 */

public class LinkFragment extends ContentFragment {
    public static final String LINKS = "Links";

    private Context ctx;

    private View linksView;

    private int pageIndex = 1;
    private int pageSize = 10;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        linksView = inflater.inflate(R.layout.fragment_link, container, false);

        ctx = linksView.getContext();

        return linksView;
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
    }
}
