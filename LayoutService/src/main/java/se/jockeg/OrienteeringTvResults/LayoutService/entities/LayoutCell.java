package se.jockeg.OrienteeringTvResults.LayoutService.entities;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonProperty;

import javax.persistence.*;
import java.io.Serializable;

@Entity
@Table(name = "layout_cell", indexes = @Index(name = "IX_CELL_ROW_ORDINAL", columnList = "row_id,ordinal", unique = true))
public class LayoutCell implements Serializable {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", unique = true, nullable = false)
    @JsonIgnore
    private Integer id;

    public Integer getId() {
        return id;
    }

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "row_id")
    private LayoutRow row;

    @Column(name= " ordinal", nullable = false)
    @JsonIgnore
    private int ordinal;

    public int getOrdinal() {
        return ordinal;
    }

    @Column(name = "className")
    private String className;

    public String getClassName() {
        return className;
    }
}
