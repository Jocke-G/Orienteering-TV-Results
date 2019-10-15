package se.jockeg.OrienteeringTvResults.LayoutService.entities;

import com.fasterxml.jackson.annotation.JsonIgnore;

import javax.persistence.*;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

@Entity
@Table(name = "layout_row", indexes = @Index(name = "IX_LAYOUT_ID_ORDINAL", columnList = "layout_id,ordinal", unique = true))
public class LayoutRow implements Serializable {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", unique = true, nullable = false)
    @JsonIgnore
    private int id;

    public int getId() {
        return id;
    }

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "layout_id")
    @JsonIgnore
    private Layout layout;

    @Column(name= " ordinal", nullable = false)
    @JsonIgnore
    private int ordinal;

    public int getOrdinal() {
        return ordinal;
    }

    @OneToMany(
            mappedBy = "row",
            cascade = CascadeType.ALL,
            orphanRemoval = true
    )
    private List<LayoutCell> cells = new ArrayList<>();

    public List<LayoutCell> getCells(){
        return cells.stream().sorted(Comparator.comparing(LayoutCell::getOrdinal)).collect(Collectors.toList());
    }
}
