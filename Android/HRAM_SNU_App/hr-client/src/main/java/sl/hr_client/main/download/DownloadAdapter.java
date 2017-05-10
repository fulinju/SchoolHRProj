package sl.hr_client.main.download;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import java.util.List;

import sl.hr_client.R;
import sl.hr_client.data.bean.DownloadBean;
import sl.hr_client.net.download.DownloadUtils;
import sl.hr_client.utils.constant.ConstantData;

/**
 * Created by Administrator on 2017/4/12.
 */

public class DownloadAdapter extends RecyclerView.Adapter {
    private Context ctx;
    private List<DownloadBean> downloadsList;

    public DownloadAdapter(Context context, List<DownloadBean> downloadsList) {
        this.ctx = context;
        this.downloadsList = downloadsList;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(ctx).inflate(R.layout.item_download, null);
        return new DownloadsHolder(view);
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        if (holder instanceof DownloadsHolder) {
            final DownloadBean temp = downloadsList.get(position);
            String dmType, dmUploadTime, uUserName;
            final String dmTitle, downloadID, dmFileURL;
            downloadID = temp.getDownloadID() == null ? ctx.getString(R.string.null_value) : temp.getDownloadID();
            dmType = temp.getDmTypeValue() == null ? ctx.getString(R.string.null_value) : temp.getDmTypeValue();
            dmTitle = temp.getDmTitle() == null ? ctx.getString(R.string.null_value) : temp.getDmTitle();
            dmUploadTime = temp.getDmUploadTime() == null ? ctx.getString(R.string.null_value) : temp.getDmUploadTime();
            dmFileURL = temp.getDmFileURL() == null ? ctx.getString(R.string.null_value) : temp.getDmFileURL();
            uUserName = temp.getUUserName() == null ? ctx.getString(R.string.null_value) : temp.getUUserName();
            int dmDownloadNum = temp == null ? 0 : temp.getDmDownloadNum();

            ((DownloadsHolder) holder).tvDownloadType.setText(String.format(ctx.getString(R.string.mid_bucket_format), dmType));
            ((DownloadsHolder) holder).tvDownloadTitle.setText(dmTitle);
            ((DownloadsHolder) holder).tvDownloadPublishTime.setText(dmUploadTime);
            ((DownloadsHolder) holder).tvDownloadUrl.setText(dmFileURL);
            ((DownloadsHolder) holder).tvDownloadUsername.setText(uUserName);
            ((DownloadsHolder) holder).tvDownloadNum.setText(String.format(ctx.getString(R.string.download_num), dmDownloadNum));

            ((DownloadsHolder) holder).btnDownload.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    DownloadUtils.downloadFile(ctx, dmFileURL, ConstantData.downloadPath, dmTitle, downloadID);
                }
            });
        }
    }

    @Override
    public int getItemCount() {
        return downloadsList == null ? 0 : downloadsList.size();
    }

    public class DownloadsHolder extends RecyclerView.ViewHolder {
        private TextView tvDownloadType;
        private TextView tvDownloadTitle;
        private TextView tvDownloadPublishTime;
        private TextView tvDownloadUrl;
        private TextView tvDownloadUsername;
        private TextView tvDownloadNum;
        private Button btnDownload;


        public DownloadsHolder(View itemView) {
            super(itemView);
            tvDownloadType = (TextView) itemView.findViewById(R.id.tv_download_type);
            tvDownloadTitle = (TextView) itemView.findViewById(R.id.tv_download_title);
            tvDownloadPublishTime = (TextView) itemView.findViewById(R.id.tv_download_publish_time);
            tvDownloadUrl = (TextView) itemView.findViewById(R.id.tv_download_url);
            tvDownloadUsername = (TextView) itemView.findViewById(R.id.tv_download_username);
            tvDownloadNum = (TextView) itemView.findViewById(R.id.tv_download_num);
            btnDownload = (Button) itemView.findViewById(R.id.btn_download);
        }
    }
}
