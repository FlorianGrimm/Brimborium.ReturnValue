//using System.Text;

//namespace Brimborium.Meaning;

//public record class MeaningDefinition(
//    string Name
//    );

//public record class MeaningDefinitionCombination(
//    string Name,
//    MeaningDefinition[] ListMeaningDefinition
//    ) : MeaningDefinition(Name) {
//    public static string CalcName(
//        MeaningDefinition[] ListMeaningDefinition
//        ) {
//        StringBuilder sb = new();
//        foreach (var item in ListMeaningDefinition) {
//            if (sb.Length > 0) sb.Append(", ");
//            sb.Append(item.ToString());
//        }
//        return sb.ToString();
//    }

//    public MeaningDefinitionCombination(
//        params MeaningDefinition[] ListMeaningDefinition
//        ) : this(CalcName(ListMeaningDefinition), ListMeaningDefinition) {
//    }

//    public MeaningDefinitionCombination(
//        string Name
//        ) : this(Name, Array.Empty<MeaningDefinition>()) {
//    }
//}

//public struct OperationScope {
//    public MeaningDefinition? Meaning;
//    public OperationScope(MeaningDefinition? Meaning = default) {
//        this.Meaning = Meaning;
//    }
//}

//public record struct Reason(string Explanation) { 
//    public static implicit operator Reason(string Explanation) =>new Reason(Explanation);
//}

//public record struct ReasonValue<T>(Reason Reason, T Value):IMeaning<T>;

