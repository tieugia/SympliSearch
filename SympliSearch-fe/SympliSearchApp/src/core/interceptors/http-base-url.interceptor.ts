import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../environments/environment';

export const HttpBaseUrlInterceptor: HttpInterceptorFn = (req, next) => {
  const apiBaseUrl = environment.apiBaseUrl;
  
  const updatedRequest = req.clone({ url: `${apiBaseUrl}${req.url}` });

  return next(updatedRequest);
};
