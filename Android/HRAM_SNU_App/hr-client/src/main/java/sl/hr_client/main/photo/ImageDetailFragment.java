package sl.hr_client.main.photo;

import android.content.Context;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ProgressBar;

import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.assist.FailReason;
import com.nostra13.universalimageloader.core.listener.SimpleImageLoadingListener;

import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.utils.ui.photoview.PhotoViewAttacher;


public class ImageDetailFragment extends Fragment {
    private Context ctx;
    private String mImageUrl;
    private ImageView mImageView;
    private ProgressBar mProgressBar;
    private PhotoViewAttacher mAttacher;

    public static ImageDetailFragment newInstance(String imageUrl) {
        final ImageDetailFragment f = new ImageDetailFragment();
        final Bundle args = new Bundle();
        args.putString("url", imageUrl);
        f.setArguments(args);
        return f;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        mImageUrl = getArguments() != null ? getArguments().getString("url") : null;
        ctx = getContext();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        final View v = inflater.inflate(R.layout.fragment_image_detail, container, false);
        mImageView = (ImageView) v.findViewById(R.id.detail_image);

        mAttacher = new PhotoViewAttacher(mImageView);
        mAttacher.setOnPhotoTapListener(new PhotoViewAttacher.OnPhotoTapListener() {
            @Override
            public void onPhotoTap(View arg0, float arg1, float arg2) {
                getActivity().finish();
            }
        });
        mProgressBar = (ProgressBar) v.findViewById(R.id.detail_img_loading);
        return v;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);

//        Glide.with(ctx)
//                .load(mImageUrl)
//                .placeholder(R.mipmap.img_load)
//                .error(R.mipmap.img_load_failed)
//                .diskCacheStrategy(DiskCacheStrategy.ALL)
//                .dontAnimate()
//                .into(mImageView);
//
//        RelativeLayout.LayoutParams params = (RelativeLayout.LayoutParams) mImageView.getLayoutParams();
//        params.width = UtilsMeasure.getScreenWidth(ctx);
//        params.height = UtilsMeasure.getScreenHeight(ctx);
//        mImageView.setLayoutParams(params);


		ImageLoader.getInstance().displayImage(mImageUrl, mImageView,
				new SimpleImageLoadingListener() {
					@Override
					public void onLoadingStarted(String imageUri, View view) {
						mProgressBar.setVisibility(View.VISIBLE);
					}

					@Override
					public void onLoadingFailed(String imageUri, View view,
							FailReason failReason) {
						String message = null;
						switch (failReason.getType()) {
						case IO_ERROR:
							message = "下载错误";
							break;
						case DECODING_ERROR:
							message = "图片无法显示";
							break;
						case NETWORK_DENIED:
							message = "网络有问题，无法下载";
							break;
						case OUT_OF_MEMORY:
							message = "图片太大无法显示";
							break;
						case UNKNOWN:
							message = "未知的错误";
							break;
						}
                        UtilsToast.showToast(ctx,message);
						mProgressBar.setVisibility(View.GONE);
					}
					@Override
					public void onLoadingComplete(String imageUri, View view,Bitmap loadedImage) {
						mProgressBar.setVisibility(View.GONE);
						mAttacher.update();
					}
				});
    }
}
