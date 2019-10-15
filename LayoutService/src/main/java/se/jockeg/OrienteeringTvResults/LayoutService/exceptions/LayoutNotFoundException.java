package se.jockeg.OrienteeringTvResults.LayoutService.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(code = HttpStatus.NOT_FOUND, reason = "Layout not found")
public class LayoutNotFoundException extends RuntimeException {
}
