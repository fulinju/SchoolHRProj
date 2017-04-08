package sl.hr_client.main.news.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.engine.DiskCacheStrategy;

import sl.base.utils.UtilsDensity;
import sl.base.utils.UtilsMeasure;
import sl.hr_client.R;
import sl.hr_client.utils.net.VolleyUtils;


/**
 * 学习圈的图片gridview
 */
public class CircleGridAdapter extends BaseAdapter {

    private String[] mFiles;
    private LayoutInflater mLayoutInflater;
    private Context ctx;


    public CircleGridAdapter(Context ctx, String[] files) {
        this.ctx = ctx;
        this.mFiles = files;
        mLayoutInflater = LayoutInflater.from(ctx);
    }

    @Override
    public int getCount() {
        return mFiles == null ? 0 : mFiles.length;
    }

    @Override
    public String getItem(int position) {
        return mFiles[position];
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        ViewHolder holder;
        if (convertView == null) {
            holder = new ViewHolder();
            convertView = mLayoutInflater.inflate(R.layout.item_gridview_image, parent, false);
            holder.imageView = (ImageView) convertView.findViewById(R.id.album_image);
            convertView.setTag(holder);
        } else {
            holder = (ViewHolder) convertView.getTag();
        }
        // 根据屏幕宽度以及图片数量动态设置图片宽高
        int width = UtilsMeasure.getScreenWidth(ctx) - UtilsDensity.dip2px(ctx,60);//减去头像的大小

        int imageWidth;
        if (mFiles.length == 1) {
            imageWidth = width / 2;
        } else if (mFiles.length <= 8 && mFiles.length >= 2) {
            imageWidth = (width / 4) - 10;
        } else {
            imageWidth = (width / 3 - 10);
        }
        holder.imageView.setLayoutParams(new LinearLayout.LayoutParams(imageWidth, imageWidth));

        String url = getItem(position);
//        ImageAware imageAware = new ImageViewAware(holder.imageView, false);
//        CommonConstants.myImageLoader.getInstance().displayImage(url, holder.imageView);

        Glide.with(ctx)
                .load(VolleyUtils.ServerIP+url)
                .placeholder(R.mipmap.img_load)
                .error(R.mipmap.img_load_failed)
                .diskCacheStrategy(DiskCacheStrategy.ALL)
                .into(holder.imageView);
//        ImageLoader.getInstance().displayImage(url,holder.imageView);

        return convertView;
    }

    private static class ViewHolder {
        ImageView imageView;
    }
}
