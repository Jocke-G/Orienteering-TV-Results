package se.jockeg.OrienteeringTvResults.LayoutService.services;

import se.jockeg.OrienteeringTvResults.LayoutService.entities.Layout;
import se.jockeg.OrienteeringTvResults.LayoutService.exceptions.LayoutNotFoundException;

public interface LayoutService {
    Iterable<Layout> getLayouts();

    Layout getLayout(String layoutName) throws LayoutNotFoundException;
}
